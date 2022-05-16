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
    public int isPlaying = -1;
    public bool PlayerCurrentlyMoving = false;
    public bool DiceAlreadyRolled = false;
    public throw_dice_2 Dice;
    private bool AllowMovement;
    public bool forward = false;
    public bool left = false;
    public bool right = false;
    public bool DirectionSelected = false;
    public bool RolledOnce = false;
    public bool positionAlreadySet = false;

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
        if(isPlaying == -1)
        {
            if (Input.GetKeyDown("1"))
            {
                isPlaying = 0;
            }
            else if (Input.GetKeyDown("2"))
            {
                isPlaying = 1;
            }
            else if (Input.GetKeyDown("3"))
            {
                isPlaying = 2;
            }
            else if (Input.GetKeyDown("4"))
            {
                isPlaying = 3;
            }
        }
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

    public IEnumerator movePlayer(GameObject Player)
    {
        Position = Player.GetComponent<PlayerInfo>().GetPosition();
        Debug.Log(Player);
        PlayerCurrentlyMoving = true;

        yield return new WaitUntil(() => Dice.finished == true);

        int DiceValue = Dice.diceValue;

        for (int NumberOfMoves = 0; NumberOfMoves < DiceValue; NumberOfMoves++)
        {
            Debug.Log("Remaining moves: " + (DiceValue - NumberOfMoves));
            foreach(GameObject field in Fields)
            {
                if(field.gameObject.transform.position == Player.gameObject.transform.position)
                {
                    Debug.Log("current Field: " + field.name);
                }
            }
 
            if(Position != 2 && Position != 10 && Position != 12 && Position != 33)
            {
                forward = true;
            }
            else
            {
                if (Position == 2 || Position == 10 || Position == 12)
                {
                    Debug.Log("Gib W für grad aus ein");
                }
                if (Position == 2 || Position == 10 || Position == 12 || Position == 33)
                {
                    Debug.Log("Gib A für links ein");
                }
                if (Position == 33)
                {
                    Debug.Log("Gib D für rechts ein");
                }
                yield return new WaitUntil(() => DirectionSelected == true);
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
 
            if (Position == 47)
            {
                Position = 19;
                Player.GetComponent<Transform>().position = Fields[Position].GetComponent<Transform>().position;
                forward = false;
            }
            else if (Position == 39)
            {
                Position = 46;
                Player.GetComponent<Transform>().position = Fields[Position].GetComponent<Transform>().position;
                forward = false;
            }
            
            else if (Position == 50)
            {
                Position = 43;
                Player.GetComponent<Transform>().position = Fields[Position].GetComponent<Transform>().position;
                forward = false;
            }
            
            else if (Position == 53)
            {
                Position = 16;
                Player.GetComponent<Transform>().position = Fields[Position].GetComponent<Transform>().position;
                forward = false;
            }
            else if (Position == 31)
            {
                Position = 0;
                Player.GetComponent<Transform>().position = Fields[Position].GetComponent<Transform>().position;
                forward = false;
            }
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
                Player.GetComponent<Transform>().position = Fields[Position].GetComponent<Transform>().position;
                forward = false;
            }
            bool isEventField = Fields[Position].GetComponent<Field>().isEventField;
            Debug.Log("Field " + Position + ": is event field: " + isEventField);
            Player.GetComponent<PlayerInfo>().SetPosition(Position);
            DirectionSelected = false;
        }
        isPlaying = -1;
        DiceAlreadyRolled = false;
        Dice.Reset();
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
            var CurrentField = $"Field ({i})";
            Fields[i] = GameObject.Find(CurrentField);
        }
        isPlaying = -1;
        PlayerCurrentlyMoving = false;
        DiceAlreadyRolled = false;
        AllowMovement = false;
        forward = false;
        left = false;
        right = false;
        DirectionSelected = false;
        RolledOnce = false;
        positionAlreadySet = false;
    }
}