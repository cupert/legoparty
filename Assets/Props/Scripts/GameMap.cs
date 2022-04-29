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
    public int Position;
    public int isPlaying;
    public bool PlayerCurrentlyMoving = false;
    public throw_dice_2 Dice;
    private bool AllowMovement;

    void Start()
    {
        Debug.Log("loaded");
        InstanciateGame();
    }



    void Update()
    {
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
        if (AllowMovement)
        {
            //if (Input.GetKeyDown("1"))
            //{
            isPlaying = 0;
            //}
            //else if (Input.GetKeyDown("2"))
            //{
            //    isPlaying = 1;
            //}
            //else if (Input.GetKeyDown("3"))
            //{
            //    isPlaying = 2;
            //}
            //else if (Input.GetKeyDown("4"))
            //{
            //    isPlaying = 3;
            //}

            //if (isPlaying == 0)
            //{
            //movePlayer(Players[0]);
            StartCoroutine(movePlayer(Players[0]));
            //}
            //else if (isPlaying == 1)
            //{
            //    movePlayer(Players[1]/*, random.Next(1, 6)*/);
            //}
            //else if (isPlaying == 2)
            //{
            //    movePlayer(Players[2]/*, random.Next(1, 6)*/);
            //}
            //else if (isPlaying == 3)
            //{
            //    movePlayer(Players[3]/*, random.Next(1, 6)*/);
            //}
            AllowMovement = false;
        }
        if (PlayerCurrentlyMoving)
        {
            if (Input.GetKeyDown("r"))
            {
                Dice.RollDice();
                PlayerCurrentlyMoving = false;
            }
        }
    }

    public IEnumerator movePlayer(GameObject Player)
    {
        Position = Player.GetComponent<PlayerInfo>().GetPosition();
        Debug.Log(Player);
        PlayerCurrentlyMoving = true;

        yield return new WaitUntil(() => Dice.finished == true);

        int DiceValue = Dice.diceValue;
        Debug.Log(DiceValue);

        /////////////////////////////
        /////////////////////////////
        // auslagern in update //////
        /////////////////////////////
        /////////////////////////////

        //for (int NumberOfMoves = 0; NumberOfMoves < DiceValue; NumberOfMoves++)
        //{
        //    if (Position == 2)
        //    {
        //        Debug.Log("Gib A für links oder W für grad aus ein");
        //        if (Input.GetKeyDown("a"))
        //        {
        //            Position = 32;
        //            Player.GetComponent<Transform>().position = Fields[Position].GetComponent<Transform>().position;
        //        }
        //        if (Input.GetKeyDown("w"))
        //        {
        //            Position++;
        //            Player.GetComponent<Transform>().position = Fields[Position].GetComponent<Transform>().position;
        //        }
        //    }
        //    else if (Position == 33)
        //    {
        //        Debug.Log("Gib A für links oder d für rechts ein");
        //        if (Input.GetKeyDown("a"))
        //        {
        //            Position++;
        //            Player.GetComponent<Transform>().position = Fields[Position].GetComponent<Transform>().position;
        //        }
        //        if (Input.GetKeyDown("d"))
        //        {
        //            Position = 40;
        //            Player.GetComponent<Transform>().position = Fields[Position].GetComponent<Transform>().position;
        //        }
        //    }
        //    else if (Position == 47)
        //    {
        //        if (Input.GetKeyDown("w"))
        //        {
        //            Position = 19;
        //            Player.GetComponent<Transform>().position = Fields[Position].GetComponent<Transform>().position;
        //        }
        //    }
        //    else if (Position == 39)
        //    {
        //        if (Input.GetKeyDown("w"))
        //        {
        //            Position = 46;
        //            Player.GetComponent<Transform>().position = Fields[Position].GetComponent<Transform>().position;
        //        }
        //    }
        //    else if (Position == 10)
        //    {
        //        Debug.Log("Gib A für links oder W für grad aus ein");
        //        if (Input.GetKeyDown("a"))
        //        {
        //            Position = 48;
        //            Player.GetComponent<Transform>().position = Fields[Position].GetComponent<Transform>().position;

        //        }
        //        if (Input.GetKeyDown("w"))
        //        {
        //            Position++;
        //            Player.GetComponent<Transform>().position = Fields[Position].GetComponent<Transform>().position;
        //        }
        //    }
        //    else if (Position == 50)
        //    {
        //        if (Input.GetKeyDown("w"))
        //        {
        //            Position = 43;
        //            Player.GetComponent<Transform>().position = Fields[Position].GetComponent<Transform>().position;
        //        }
        //    }
        //    else if (Position == 12)
        //    {
        //        Debug.Log("Gib A für links oder W für grad aus ein");
        //        if (Input.GetKeyDown("a"))
        //        {
        //            Position = 51;
        //            Player.GetComponent<Transform>().position = Fields[Position].GetComponent<Transform>().position;

        //        }
        //        if (Input.GetKeyDown("w"))
        //        {
        //            Position++;
        //            Player.GetComponent<Transform>().position = Fields[Position].GetComponent<Transform>().position;
        //        }
        //    }
        //    else if (Position == 53)
        //    {
        //        if (Input.GetKeyDown("w"))
        //        {
        //            Position = 16;
        //            Player.GetComponent<Transform>().position = Fields[Position].GetComponent<Transform>().position;

        //        }
        //    }
        //    else if (Position == 31)
        //    {
        //        if (Input.GetKeyDown("w"))
        //        {
        //            Position = 0;
        //            Player.GetComponent<Transform>().position = Fields[Position].GetComponent<Transform>().position;

        //        }
        //    }
        //    else
        //    {
        //        Debug.Log("Else");
        //        while (Input.GetMouseButton(0))
        //        {
        //            if (Input.GetKeyDown("w"))
        //            {
        //                Debug.Log("bitte funktioniere wieder");

        //                if (Position < Config.numberofFields - 1)
        //                {
        //                    Position++;
        //                }
        //                else
        //                {
        //                    Position = 0;
        //                }
        //                //Debug.Log(count);
        //                Player.GetComponent<Transform>().position = Fields[Position].GetComponent<Transform>().position;
        //            }
        //        }
        //    }
        //    Player.GetComponent<PlayerInfo>().SetPosition(Position);
        //}
    }
    void InstanciateGame()
    {
        Debug.Log("InsanciateGame");
        Dice = GameObject.Find("dice_with_colliders 1").GetComponent<throw_dice_2>();
        Players[0] = GameObject.Find("Yellow");
        Players[1] = GameObject.Find("Blue");
        Players[2] = GameObject.Find("Red");
        Players[3] = GameObject.Find("White");

        for (int i = 0; i < Config.numberofFields; i++)
        {
            if (i > 31 && i < 40)
            {
                var CurrentField = $"Field ({i + 8})";
                Fields[i] = GameObject.Find(CurrentField);
                //Debug.Log(CurrentField);
            }
            else if (i > 39 && i < 48)
            {
                var CurrentField = $"Field ({i + 10})";
                Fields[i] = GameObject.Find(CurrentField);
                //Debug.Log(CurrentField);
            }
            else if (i > 47 && i < 51)
            {
                var CurrentField = $"Field ({i + 12})";
                Fields[i] = GameObject.Find(CurrentField);
                //Debug.Log(CurrentField);
            }
            else if (i > 50)
            {
                var CurrentField = $"Field ({i + 19})";
                Fields[i] = GameObject.Find(CurrentField);
                //Debug.Log(CurrentField);
            }
            else
            {
                var CurrentField = $"Field ({i})";
                Fields[i] = GameObject.Find(CurrentField);
                //Debug.Log(CurrentField);
            }
        }
    }
}