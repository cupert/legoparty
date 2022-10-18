using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using Object = System.Object;
using Random = UnityEngine.Random;

public class GameMap : MonoBehaviour
{
    public GameObject[] Fields = new GameObject[Config.numberofFields];
    public GameObject[] BaseFields = new GameObject[Config.numberofBaseFields];
    //public GameObject[] Players = new GameObject[4]; 
    public List<GameObject> PlayersList; 
    public int Position; // current & new player position
    public int isPlaying = -1; // indicates which player is currently playing, -1 for none
    public bool PlayerCurrentlyMoving = false;
    public bool DiceAlreadyRolled = false; // player has rolled the dice for their turn
    public throw_dice_2 Dice; // dice connected to player movement 
    private bool AllowMovement; // allows the player to move
    public bool forward = false;
    public bool left = false;
    public bool right = false;
    public bool DirectionSelected = false; // used when waiting on user input at crossroads
    public bool RolledOnce = false; // dice has been rolled at least once in the game
    public bool positionAlreadySet = false;
    private float _movementTime = 1; //the time in seconds it takes for the figurine to move to each field
    public bool isShuffled = false;
    public bool isJunction = false;
    public bool characterSelectionIsActive = false;
    public bool addingCharacter = false;
    public bool yellowAlreadyChosen = false;
    public bool whiteAlreadyChosen = false;
    public bool blueAlreadyChosen = false;
    public bool redAlreadyChosen = false;
    public GameObject Trophy;
    public GameObject itemPanel; // item menu UI panel, this is opened and closed when on item store field
    public LoadScene SceneLoader;

    void Start()
    {
        Debug.Log("game loaded");
        InstanciateGame();
    }

    void Update()
    {
        //character Selection
        if (characterSelectionIsActive)
        {
            //add a player; up to 4 players possible
            if (Input.GetKeyDown("z") && !addingCharacter && PlayersList.Count < 4)
            {
                Debug.Log("Choose your color by pressing \"u\" (yellow), \"i\" (red), \"o\" (white) or \"p\" (blue).");
                addingCharacter = true;
            }
            //choosing yellow
            else if (Input.GetKeyDown("u") && addingCharacter)
            {
                if (yellowAlreadyChosen)
                {
                    Debug.Log("Another Player already is yellow, please choose a different color.");
                }
                else
                {
                    PlayersList.Add(GameObject.Find("Yellow"));
                    Debug.Log($"Player {PlayersList.Count} chose yellow");
                    Debug.Log("Press \"z\" to add a another character or press \"x\" to start the game.");
                    yellowAlreadyChosen = true;
                    addingCharacter = false;
                }   
            }
            //choosing red
            else if(Input.GetKeyDown("i") && addingCharacter)
            {
                if (redAlreadyChosen)
                {
                    Debug.Log("Another Player already is red, please choose a different color.");
                }
                else
                {
                    PlayersList.Add(GameObject.Find("Red"));
                    Debug.Log($"Player {PlayersList.Count} chose red");
                    Debug.Log("Press \"z\" to add a another character or press \"x\" to start the game.");
                    redAlreadyChosen = true;
                    addingCharacter = false;
                }
            }
            //choosing white
            else if (Input.GetKeyDown("o") && addingCharacter)
            {
                if (whiteAlreadyChosen)
                {
                    Debug.Log("Another Player already is white, please choose a different color.");
                }
                else
                {
                    PlayersList.Add(GameObject.Find("White"));
                    Debug.Log($"Player {PlayersList.Count} chose white");
                    Debug.Log("Press \"z\" to add a another character or press \"x\" to start the game.");
                    whiteAlreadyChosen = true;
                    addingCharacter = false;
                }
            }
            //choosing blue
            else if (Input.GetKeyDown("p") && addingCharacter)
            {
                if (blueAlreadyChosen)
                {
                    Debug.Log("Another Player already is blue, please choose a different color.");
                }
                else
                {
                    PlayersList.Add(GameObject.Find("Blue"));
                    Debug.Log($"Player {PlayersList.Count} chose blue");
                    Debug.Log("Press \"z\" to add a another character or press \"x\" to start the game.");
                    blueAlreadyChosen = true;
                    addingCharacter = false;
                }
            }
            //end character selection early
            else if (Input.GetKeyDown("x"))
            {
                Debug.Log("Character selection has ended.");
                characterSelectionIsActive = false;
                ShufflePlayerArray();
            }
            //end character selection when maximum number of players has been reached
            else if (PlayersList.Count >= 4)
            {
                Debug.Log("Character selection has ended.");
                characterSelectionIsActive = false;
                ShufflePlayerArray();
            }
        }

        if (AllowMovement && isPlaying != -1)
        {
            if (isPlaying == 0 && DiceAlreadyRolled)
            {
                //StartCoroutine(movePlayer(Players[0]));
                //AllowMovement = false;
                StartCoroutine(movePlayer(PlayersList[0]));
                AllowMovement = false;
            }
            else if (isPlaying == 1 && DiceAlreadyRolled)
            {
                //StartCoroutine(movePlayer(Players[1]));
                //AllowMovement = false;
                StartCoroutine(movePlayer(PlayersList[1]));
                AllowMovement = false;
            }
            else if (isPlaying == 2 && DiceAlreadyRolled)
            {
                //StartCoroutine(movePlayer(Players[2]));
                //AllowMovement = false;
                StartCoroutine(movePlayer(PlayersList[2]));
                AllowMovement = false;
            }
            else if (isPlaying == 3 && DiceAlreadyRolled)
            {
                //StartCoroutine(movePlayer(Players[3]));
                //AllowMovement = false;
                StartCoroutine(movePlayer(PlayersList[3]));
                AllowMovement = false;
            } 
        }

        // roll dice with 'r' to move the player the rolled number of fields
        if (Input.GetKeyDown("r") && !DiceAlreadyRolled)
        {
            Dice.RollDice();
            RolledOnce = true;
            DiceAlreadyRolled = true;
        }
        
        if (PlayerCurrentlyMoving && isPlaying != -1)
        {
            // select direction when at a crossroad
            if (Input.GetKeyDown("w"))
            {
                //only allow forward movement in the following cases
                if (Position == 2 || Position == 10 || Position == 12)
                {
                    forward = true;
                    DirectionSelected = true;
                }
            }
            else if (Input.GetKeyDown("a"))
            {
                //only allow movement to the left in the following cases
                if (Position == 2 || Position == 10 || Position == 12 || Position == 33)
                {
                    left = true;
                    DirectionSelected = true;
                }
            }
            else if (Input.GetKeyDown("d"))
            {
                //only allow movement to the right in the following cases
                if (Position == 33)
                {
                    right = true;
                    DirectionSelected = true;
                }
                
            }
        }
    }

    // logic behind moving a player
    public IEnumerator movePlayer(GameObject Player)
    {
        Debug.Log("Coroutine movePlayer started");
        // get the selected player's current position
        Position = Player.GetComponent<PlayerInfo>().position;
        Debug.Log(Player);
        Debug.Log(Position);
        PlayerCurrentlyMoving = true;

        // wait until dice has finished rolling to get valid dice value
        yield return new WaitUntil(() => Dice.finished == true);

        int DiceValue = Dice.diceValue;

        // loop through player moves according to dice value since player moves automatically
        for (int NumberOfMoves = 0; NumberOfMoves < DiceValue; NumberOfMoves++)
        {
            Debug.Log("Remaining moves: " + (DiceValue - NumberOfMoves));
            foreach(GameObject field in Fields)
            {
                if(field.gameObject.transform.position == Player.gameObject.transform.position)
                {
                    Debug.Log("Current Field: " + field.name);
                }
            }
 
            // player is at non-crossroad field
            if(Position != 2 && Position != 10 && Position != 12 && Position != 33)
            {
                forward = true;
            }
            // player is at a cross road
            else
            {
                if (Position == 2 || Position == 10 || Position == 12)
                {
                    Debug.Log("Press 'w' to go forward");
                }
                if (Position == 2 || Position == 10 || Position == 12 || Position == 33)
                {
                    Debug.Log("Press 'a' to go left");
                }
                if (Position == 33)
                {
                    Debug.Log("Press 'd' to go right");
                }
                isJunction = true;
                yield return new WaitUntil(() => DirectionSelected == true);
                isJunction = false;
                
                // move player away from crossroad according to directional input 
                if (Position == 2)
                {
                    if (left)
                    {
                        Position = 32;
                        left = false;
                    }
                    if (forward)
                    {
                        Position++;
                        forward = false;
                    }
                }
                else if (Position == 33)
                {
                    if (left)
                    {
                        Position++;
                        left = false;
                    }
                    if (right)
                    {
                        Position = 40;
                        right = false;
                    }
                }
                else if (Position == 10)
                {
                    if (left)
                    {
                        Position = 48;
                        left = false;

                    }
                    if (forward)
                    {
                        Position++;
                        forward = false;
                    }
                }
                else if (Position == 12)
                {
                    if (left)
                    {
                        Position = 51;
                        left = false;
                    }
                    if (forward)
                    {
                        Position++;
                        forward = false;
                    }
                }
                positionAlreadySet = true;
            }
 
            // positions 47, 39, 50, 53 are at the end of a crossroad section and the player
            // needs to get back to the "main" section
            if (Position == 47)
            {
                Position = 19;
                forward = false;
            }
            else if (Position == 39)
            {
                Position = 46;
                forward = false;
            }
            
            else if (Position == 50)
            {
                Position = 43;
                forward = false;
            }
            
            else if (Position == 53)
            {
                Position = 16;
                forward = false;
            }

            // field 31 is before field 0 geographically on the map, not a crossroad
            else if (Position == 31)
            {
                Position = 0;
                forward = false;
            }
            // move player normally (position + 1)
            else
            {
                if (!positionAlreadySet)
                {
                    if (Position < Config.numberofFields - 1)
                    {
                        Position++;
                    }
                    else
                    {
                        Position = 0;
                    }
                }
                positionAlreadySet = false;
                forward = false;
            }

            // move player to specified position geographically
            int nextPosition = Position;    //prevent changes in position value during coroutine by using intended next position
            float progression;              //how fas the animation advanced
            Vector3 fieldOffset = Vector3.down * 2;
            AnimationCurve curve = AnimationCurve.Constant(0, _movementTime, 3);
            for (float passedTime = 0; passedTime <= _movementTime; passedTime += Time.fixedDeltaTime)
            {
                //progression ranges from 0 to 1 over the course of the animation
                progression = passedTime / _movementTime;

                //interpolate horizontal movement over time
                Vector3 deltaPosition = Vector3.Lerp(Player.transform.position, Fields[nextPosition].transform.position + fieldOffset, passedTime);
                //interpolate vertical movement over time
                deltaPosition.y += curve.Evaluate(passedTime);

                //apply positional value over time
                Player.transform.position = deltaPosition;
                yield return new WaitForFixedUpdate();
            }

            Player.GetComponent<PlayerInfo>().position = Position;
            DirectionSelected = false;
            //interact with fields during the turn
            if (Fields[Position].GetComponent<IField>().FieldType == FieldTypeEnum.BaseField)
            {
                if (Fields[Position].GetComponent<BaseField>().IsTrophyField == true)
                {
                    Fields[Position].GetComponent<BaseField>().GiveTrophy(Player.GetComponent<PlayerInfo>());
                    PlaceTrophy();
                }
            }
            // player passes/lands on item shop field
            if (Fields[Position].GetComponent<IField>().FieldType == FieldTypeEnum.ItemShopField)
            {
                Fields[Position].GetComponent<ItemShopField>().InteractWithPlayer(Player.GetComponent<PlayerInfo>());
                itemPanel.GetComponent<openItemPanel>().OpenAndClose();
                // yield return new WaitUntil(() => Fields[Position].GetComponent<ItemShopField>().IsDoneShopping == true);

                // TODO: close panel also on selection not only close button click
                yield return new WaitUntil(() => itemPanel.GetComponent<openItemPanel>().active == false);
                Fields[Position].GetComponent<ItemShopField>().IsDoneShopping = false;
            }
        }
        //interact with fields at the end of the turn
        if(Fields[Position].GetComponent<IField>().FieldType != FieldTypeEnum.ItemShopField)
        {
            Fields[Position].GetComponent<IField>().InteractWithPlayer(Player.GetComponent<PlayerInfo>());
            Debug.Log(Player + " coins " + Player.GetComponent<PlayerInfo>().coins);
            Debug.Log(Player + " trophies " + Player.GetComponent<PlayerInfo>().trophies);
        }

        //if (isPlaying == 3)
        //{
        //    SceneLoader.LoadRandomScene();
        //    isPlaying = 0;
        //}
        if (isPlaying == (PlayersList.Count - 1))
        {
            SceneLoader.LoadRandomScene();
            isPlaying = 0;
        }
        else
        {
            isPlaying++;
        }
        DiceAlreadyRolled = false;
        Dice.Reset();

        //allow movement at the end of the coroutine
        AllowMovement = true;
        PlayerCurrentlyMoving = false;

        UIManager.UIManagerRef.IncreaseTurnCount();
    }

    // instanciates dice, players, fields and values needed for the game
    void InstanciateGame()
    {
        Debug.Log("InsanciateGame");
        // instanciate dice
        Dice = GameObject.Find("dice_with_colliders 1").GetComponent<throw_dice_2>();
        // instanciate players 1-4
        //Players[0] = GameObject.Find("Yellow");
        //Players[1] = GameObject.Find("Blue");
        //Players[2] = GameObject.Find("Red");
        //Players[3] = GameObject.Find("White");
        SelectPlayersCharacters();

        // instanciate map fields
        for (int i = 0; i < Config.numberofFields; i++)
        {
            var CurrentField = $"Field ({i})";
            Fields[i] = GameObject.Find(CurrentField);
        }
        
        int k = 0;
        for (int i = 0; i < Fields.Length; i++)
        {
            if (Fields[i].GetComponent<IField>().FieldType == FieldTypeEnum.BaseField)
            {
                BaseFields[k] = Fields[i];
                k++;
            }
        }

        // instanciate bools reflecting that no player has played yet and is not allowed
        // to move or roll the dice
        isPlaying = 0;
        PlayerCurrentlyMoving = false;
        DiceAlreadyRolled = false;
        AllowMovement = true;
        forward = false;
        left = false;
        right = false;
        DirectionSelected = false;
        RolledOnce = false;
        positionAlreadySet = false;
        isShuffled = false;
        isJunction = false;
        //ShufflePlayerArray();
        Trophy = Instantiate(Trophy);
        PlaceTrophy();
    }

    void ShufflePlayerArray()
    {
        Debug.Log("Determinating turn sequence.");
        GameObject tempPlayer;
        //for (int i = 0; i < Players.Length; i++)
        //{
        //    int rnd = Random.Range(0, Players.Length);
        //    tempPlayer = Players[rnd];
        //    Players[rnd] = Players[i];
        //    Players[i] = tempPlayer;
        //}
        for (int i = 0; i < PlayersList.Count; i++)
        {
            int rnd = Random.Range(0, PlayersList.Count);
            tempPlayer = PlayersList[rnd];
            PlayersList[rnd] = PlayersList[i];
            PlayersList[i] = tempPlayer;
        }

        //for(int i = 0; i < Players.Length; i++)
        //{
        //    Players[i].GetComponent<PlayerInfo>().PlayerNumber = (short)(i + 1);
        //}
        for (int i = 0; i < PlayersList.Count; i++)
        {
            PlayersList[i].GetComponent<PlayerInfo>().PlayerNumber = (short)(i + 1);
        }
        Debug.Log($"Turn sequence:");
        for (int i = 0; i < PlayersList.Count; i++)
        {
            Debug.Log($"{i + 1}: {PlayersList[i]}");
        }
        isShuffled = true;
    }

    void PlaceTrophy()
    {
        int rnd = Random.Range(0, BaseFields.Length);
        BaseFields[rnd].GetComponent<BaseField>().IsTrophyField = true;
        Debug.Log("Trophy " + BaseFields[rnd].name);

        Trophy.transform.position = BaseFields[rnd].transform.position;
    }

    void SelectPlayersCharacters()
    {
        Debug.Log("CharacterSelection");
        //PlayersList.Add(GameObject.Find("Yellow"));
        //PlayersList.Add(GameObject.Find("Blue"));
        //PlayersList.Add(GameObject.Find("Red"));
        //PlayersList.Add(GameObject.Find("White"));
        Debug.Log("Press \"z\" to add a character. (up to 4)");
        characterSelectionIsActive = true;
    }
}