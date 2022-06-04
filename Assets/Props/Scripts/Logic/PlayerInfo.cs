using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerInfo : MonoBehaviour
{
    private int _coins;
    private int _trophies;
    private float _increaseDelay = 0.1f;

    //public GameObject Field;
    public int position { get; set; }
    public short PlayerNumber { get; set; }

    public int coins
    {
        get => _coins;
        set
        {
            if (value > _coins)
            {
                StartCoroutine(IncreaseCoinsOverTime(value - _coins));
            }
            else if (value < _coins)
            {
                StartCoroutine(DecreaseCoinsOverTime(_coins - value));
            }
            else
            {
                UIManager.UIManagerRef.SetCoinCount(PlayerNumber, value);
            }

            _coins = value;
        }
    }

    public int trophies
    {
        get => _trophies;
        set
        {
            if (value > _trophies)
            {
                StartCoroutine(IncreaseTrophiesOverTime(value - _trophies));
            }
            else if (value < _trophies)
            {
                StartCoroutine(DecreaseTrophiesOverTime(_trophies - value));
            }
            else
            {
                UIManager.UIManagerRef.SetTrophyCount(PlayerNumber, value);
            }

            _trophies = value;
        }
    }

    void Start()
    {
        position = -1;
        coins = 0;
        trophies = 0;
    }

    IEnumerator IncreaseCoinsOverTime(int difference)
    {
        for(int i = 0; i < difference; i++)
        { 
            UIManager.UIManagerRef.IncreaseCoinCount(PlayerNumber);
            yield return new WaitForSeconds(_increaseDelay);
        }
    }

    IEnumerator DecreaseCoinsOverTime(int difference)
    {
        for (int i = 0; i < difference; i++)
        {
            UIManager.UIManagerRef.DecreaseCoinCount(PlayerNumber);
            yield return new WaitForSeconds(_increaseDelay);
        }
    }

    IEnumerator IncreaseTrophiesOverTime(int difference)
    {
        for (int i = 0; i < difference; i++)
        {
            UIManager.UIManagerRef.IncreaseTrophyCount(PlayerNumber);
            yield return new WaitForSeconds(_increaseDelay);
        }
    }

    IEnumerator DecreaseTrophiesOverTime(int difference)
    {
        for (int i = 0; i < difference; i++)
        {
            UIManager.UIManagerRef.DecreaseTrophyCount(PlayerNumber);
            yield return new WaitForSeconds(_increaseDelay);
        }
    }
}

