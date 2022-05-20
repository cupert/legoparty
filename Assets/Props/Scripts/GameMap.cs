using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UIElements;
using Object = System.Object;


public class GameMap : MonoBehaviour
{
    public GameObject[] Fields = new GameObject[Config.numberofFields];
    public GameObject[] Players = new GameObject[4]; 
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

    void Start()
    {
        Debug.Log("loaded");
        InstanciateGame();
    }


    void Update()
    {
        // start player turn with 's'
        if (Input.GetKeyDown("s"))
        {
            if (AllowMovement)
            {
                AllowMovement = false;
            }
            else
            {
                AllowMovement = true;
            }
            Debug.Log(AllowMovement);
        }
        // turn has started and player has been selected
        //if(isPlaying == -1)
        //{
        //    if (Input.GetKeyDown("1"))
        //    {
        //        isPlaying = 0;
        //    }
        //    else if (Input.GetKeyDown("2"))
        //    {
        //        isPlaying = 1;
        //    }
        //    else if (Input.GetKeyDown("3"))
        //    {
        //        isPlaying = 2;
        //    }
        //    else if (Input.GetKeyDown("4"))
        //    {
        //        isPlaying = 3;
        //    }
        //}
        if (AllowMovement && isPlaying != -1)
        {
            if (isPlaying == 0)
            {
                StartCoroutine(movePlayer(Players[0]));
                AllowMovement = false;
            }
            else if (isPlaying == 1)
            {
                StartCoroutine(movePlayer(Players[1]));
                AllowMovement = false;
            }
            else if (isPlaying == 2)
            {
                StartCoroutine(movePlayer(Players[2]));
                AllowMovement = false;
            }
            else if (isPlaying == 3)
            {
                StartCoroutine(movePlayer(Players[3]));
                AllowMovement = false;
            } 
        }
        
        // roll dice with 'r' to move the player the rolled number of fields
        if (PlayerCurrentlyMoving && isPlaying != -1)
        {
            if (Input.GetKeyDown("r") && !DiceAlreadyRolled)
            {
                if (RolledOnce) 
                {
                    Dice.RollAgain();
                }
                Dice.RollDice();
                RolledOnce = true;
                DiceAlreadyRolled = true;
            }

            // select direction when at a crossroad
            if (Input.GetKeyDown("w"))
            {
                forward = true;
                DirectionSelected = true;
            }
            else if (Input.GetKeyDown("a"))
            {
                left = true;
                DirectionSelected = true;
            }
            else if (Input.GetKeyDown("d"))
            {
                right = true;
                DirectionSelected = true;
            }
        }
    }

    // logic behind moving a player
    public IEnumerator movePlayer(GameObject Player)
    {
        // get the selected player's current position
        Position = Player.GetComponent<PlayerInfo>().GetPosition();
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
            if(Position != 2 && Position != 10 
                && Position != 12 && Position != 33)
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
                if (Position == 2 || Position == 10 
                    || Position == 12 || Position == 33)
                {
                    Debug.Log("Press 'a' to go left");
                }
                if (Position == 33)
                {
                    Debug.Log("Press 'd' to go right");
                }
                yield return new WaitUntil(() => DirectionSelected == true);
                
                // move player away from crossroad according to directional input 
                if (Position == 2)
                {
                    if (left)
                    {
                        Position = 32;
                        Player.GetComponent<Transform>().position = Fields[Position].GetComponent<Transform>().position;
                        left = false;
                    }
                    if (forward)
                    {
                        Position++;
                        Player.GetComponent<Transform>().position = Fields[Position].GetComponent<Transform>().position;
                        forward = false;
                    }
                }
                else if (Position == 33)
                {
                    if (left)
                    {
                        Position++;
                        Player.GetComponent<Transform>().position = Fields[Position].GetComponent<Transform>().position;
                        left = false;
                    }
                    if (right)
                    {
                        Position = 40;
                        Player.GetComponent<Transform>().position = Fields[Position].GetComponent<Transform>().position;
                        right = false;
                    }
                }
                else if (Position == 10)
                {
                    if (left)
                    {
                        Position = 48;
                        Player.GetComponent<Transform>().position = Fields[Position].GetComponent<Transform>().position;
                        left = false;

                    }
                    if (forward)
                    {
                        Position++;
                        Player.GetComponent<Transform>().position = Fields[Position].GetComponent<Transform>().position;
                        forward = false;
                    }
                }
                else if (Position == 12)
                {
                    if (left)
                    {
                        Position = 51;
                        Player.GetComponent<Transform>().position = Fields[Position].GetComponent<Transform>().position;
                        left = false;
                    }
                    if (forward)
                    {
                        Position++;
                        Player.GetComponent<Transform>().position = Fields[Position].GetComponent<Transform>().position;
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
                Player.GetComponent<Transform>().position = Fields[Position].
                GetComponent<Transform>().position;
                forward = false;
            }
            else if (Position == 39)
            {
                Position = 46;
                Player.GetComponent<Transform>().position = Fields[Position].
                GetComponent<Transform>().position;
                forward = false;
            }
            
            else if (Position == 50)
            {
                Position = 43;
                Player.GetComponent<Transform>().position = Fields[Position].
                GetComponent<Transform>().position;
                forward = false;
            }
            
            else if (Position == 53)
            {
                Position = 16;
                Player.GetComponent<Transform>().position = Fields[Position].
                GetComponent<Transform>().position;
                forward = false;
            }

            // field 31 is before field 0 geographically on the map, not a crossroad
            else if (Position == 31)
            {
                Position = 0;
                Player.GetComponent<Transform>().position = Fields[Position].
                GetComponent<Transform>().position;
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
                Player.GetComponent<Transform>().position = Fields[Position].
                GetComponent<Transform>().position;
                forward = false;
            }
            // bool isEventField = Fields[Position].GetComponent<Field>().isEventField;
            // Debug.Log("Field " + Position + ": is event field: " + isEventField);

            // move player to specified position geographically
            Player.GetComponent<PlayerInfo>().SetPosition(Position);
            DirectionSelected = false;
        }
        //interact with field
        if(isPlaying == 3)
        {
            //StartMiniGame()
            isPlaying = 0;
        }
        else
        {
            isPlaying++;
        }
        DiceAlreadyRolled = false;
        Dice.Reset();
    }

    // instanciates dice, players, fields and values needed for the game
    void InstanciateGame()
    {
        Debug.Log("InsanciateGame");
        // instanciate dice
        Dice = GameObject.Find("dice_with_colliders 1").GetComponent<throw_dice_2>();
        // instanciate players 1-4
        Players[0] = GameObject.Find("Yellow");
        Players[1] = GameObject.Find("Blue");
        Players[2] = GameObject.Find("Red");
        Players[3] = GameObject.Find("White");

        // instanciate map fields
        for (int i = 0; i < Config.numberofFields; i++)
        {
            var CurrentField = $"Field ({i})";
            Fields[i] = GameObject.Find(CurrentField);
        }

        // instanciate bools reflecting that no player has played yet and is not allowed
        // to move or roll the dice
        isPlaying = 0;
        PlayerCurrentlyMoving = false;
        DiceAlreadyRolled = false;
        AllowMovement = false;
        forward = false;
        left = false;
        right = false;
        DirectionSelected = false;
        RolledOnce = false;
        positionAlreadySet = false;
        //DetermineTurnSequence();
    }

    void DetermineTurnSequence()
    {
        //player one rolls dice
        //add to array
        //player two rolls dice
        //if player two rolls same number -> roll again, elseif player two rolls less -> append, else -> prepend
        //player three rolls dice
        //if player three rolls same number as player one or player two -> roll again, ......

    }

    //IEnumerator DetermineTurnSequence()
    //{
    //    int[] playerDiceValues = new int[4];
    //    int i = 0;
    //    GameObject[] players = Players;
    //    foreach (GameObject player in Players)
    //    {
    //        yield return new WaitUntil(() => Dice.finished == true);
    //        playerDiceValues[i] = Dice.diceValue;
    //        i++;
    //    }
    //    if(playerDiceValues[0] != playerDiceValues[1] &&
    //        playerDiceValues[0] != playerDiceValues[2] &&
    //        playerDiceValues[0] != playerDiceValues[3] &&
    //        playerDiceValues[1] != playerDiceValues[2] &&
    //        playerDiceValues[1] != playerDiceValues[3] &&
    //        playerDiceValues[2] != playerDiceValues[3])
    //    {
    //        Players[0] = players[0];
    //        if (playerDiceValues[0] < playerDiceValues[1])
    //        {
    //            Players[1] = Players[0];
    //            Players[0] = players[1];
    //        }
    //        else if (playerDiceValues[0] > playerDiceValues[1])
    //        {
    //            Players[1] = players[1];
    //        }
    //        else
    //        {
    //            Debug.Log("Error");
    //        }
    //        if(playerDiceValues[0] < playerDiceValues[2] && playerDiceValues[1] < playerDiceValues[2])
    //        {
    //            Players[2] = Players[1];
    //            Players[1] = Players[0];
    //            Players[0] = players[2];
    //        }
    //        else if (playerDiceValues[0] > playerDiceValues[2] && playerDiceValues[1] > playerDiceValues[2])
    //        {
    //            Players[2] = players[2];
    //        }
    //        else if (playerDiceValues[0] > playerDiceValues[2] && playerDiceValues[1] < playerDiceValues[2])
    //        {
    //            Players[2] = Players[1];
    //            Players[1] = players[2];
    //        }
    //        else
    //        {
    //            Debug.Log("Error");
    //        }
    //        if (playerDiceValues[0] < playerDiceValues[3] && playerDiceValues[1] < playerDiceValues[3] && playerDiceValues[2] < playerDiceValues[3])
    //        {
    //            Players[3] = Players[2];
    //            Players[2] = Players[1];
    //            Players[1] = Players[0];
    //            Players[0] = players[3];
    //        }
    //        else if (playerDiceValues[0] > playerDiceValues[3] && playerDiceValues[1] > playerDiceValues[3] && playerDiceValues[2] > playerDiceValues[3])
    //        {
    //            Players[3] = players[3];
    //        }
    //        else if (playerDiceValues[0] > playerDiceValues[3] && playerDiceValues[1] < playerDiceValues[3])
    //        {
    //            Players[3] = Players[2];
    //            Players[2] = Players[1];
    //            Players[1] = players[3];
    //        }
    //        else if (playerDiceValues[1] > playerDiceValues[3] && playerDiceValues[2] < playerDiceValues[3])
    //        {
    //            Players[3] = Players[2];
    //            Players[2] = players[3];
    //        }
    //        else
    //        {
    //            Debug.Log("Error");
    //        }
    //    }
    //    else
    //    {
    //        //zwei oder mehr haben das selbe gewürfelt
    //        if(playerDiceValues[0] == playerDiceValues[1] && playerDiceValues[0] == playerDiceValues[2] && playerDiceValues[0] == playerDiceValues[3])
    //        {
    //            DetermineTurnSequence();
    //        }
    //        else if(playerDiceValues[0] == playerDiceValues[1])
    //        {
    //            for (int k = 0; k < 2; k++)
    //            {
    //                yield return new WaitUntil(() => Dice.finished == true);
    //                playerDiceValues[k] = Dice.diceValue;
    //            }
    //        }
    //        else if (playerDiceValues[0] == playerDiceValues[2])
    //        {
    //            for (int k = 0; k < 3; k++)
    //            {
    //                yield return new WaitUntil(() => Dice.finished == true);
    //                playerDiceValues[k] = Dice.diceValue;
    //                k++;
    //            }
    //        }
    //        else if (playerDiceValues[0] == playerDiceValues[3])
    //        {
    //            for (int k = 0; k < 4; k++)
    //            {
    //                yield return new WaitUntil(() => Dice.finished == true);
    //                playerDiceValues[k] = Dice.diceValue;
    //                k++;
    //                k++;
    //            }
    //        }
    //        else if (playerDiceValues[1] == playerDiceValues[2])
    //        {
    //            for (int k = 1; k < 3; k++)
    //            {
    //                yield return new WaitUntil(() => Dice.finished == true);
    //                playerDiceValues[k] = Dice.diceValue;
    //            }
    //        }
    //        else if (playerDiceValues[1] == playerDiceValues[3])
    //        {

    //        }
    //        else if (playerDiceValues[2] == playerDiceValues[3])
    //        {

    //        }
    //        else if (playerDiceValues[0] == playerDiceValues[1] && playerDiceValues[0] == playerDiceValues[2])
    //        {

    //        }
    //        else if (playerDiceValues[0] == playerDiceValues[1] && playerDiceValues[0] == playerDiceValues[3])
    //        {

    //        }
    //        else if (playerDiceValues[0] == playerDiceValues[2] && playerDiceValues[0] == playerDiceValues[3])
    //        {

    //        }
    //        else if (playerDiceValues[1] == playerDiceValues[2] && playerDiceValues[1] == playerDiceValues[3])
    //        {

    //        }
    //        else
    //        {
    //            Debug.Log("Error");
    //        }
    //    }
    //}
}