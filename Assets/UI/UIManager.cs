using System;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    private int _turnCount = 1;
    [SerializeField]
    private TextMeshProUGUI _turnText;

    [SerializeField]
    private TextMeshProUGUI _p1CoinCount;

    [SerializeField]
    private TextMeshProUGUI _p2CoinCount;

    [SerializeField]
    private TextMeshProUGUI _p3CoinCount;

    [SerializeField]
    private TextMeshProUGUI _p4CoinCount;

    [SerializeField]
    private TextMeshProUGUI _p1TrophyCount;

    [SerializeField]
    private TextMeshProUGUI _p2TrophyCount;

    [SerializeField]
    private TextMeshProUGUI _p3TrophyCount;

    [SerializeField]
    private TextMeshProUGUI _p4TrophyCount;

    private int _numUpperLimit = 999;
    private int _numLowerLimit = 0;

    public static UIManager UIManagerRef;

    private void Awake()
    {
        //set static reference to script for other classes to use
        UIManagerRef = this;
    }
    
    void Start()
    {
        _turnText.text = $"Turn {_turnCount}";
    }

    public void IncreaseTurnCount()
    {
        _turnCount++;
        _turnText.text = $"Turn {_turnCount}";
    }

    public void IncreaseCoinCount(short playerNumber)
    {
        int tmpCount = 0;
        switch (playerNumber)
        {
            case 1:
                tmpCount = Int32.Parse(_p1CoinCount.text);
                tmpCount++;
                if (tmpCount > _numUpperLimit)
                {
                    tmpCount = _numUpperLimit;
                }
                _p1CoinCount.text = "  " + tmpCount;
                break;

            case 2:
                tmpCount = Int32.Parse(_p2CoinCount.text);
                tmpCount++;
                if (tmpCount > _numUpperLimit)
                {
                    tmpCount = _numUpperLimit;
                }
                _p2CoinCount.text = "  " + tmpCount;
                break;

            case 3:
                tmpCount = Int32.Parse(_p3CoinCount.text);
                tmpCount++;
                if (tmpCount > _numUpperLimit)
                {
                    tmpCount = _numUpperLimit;
                }
                _p3CoinCount.text = "  " + tmpCount;
                break;

            case 4:
                tmpCount = Int32.Parse(_p4CoinCount.text);
                tmpCount++;
                if (tmpCount > _numUpperLimit)
                {
                    tmpCount = _numUpperLimit;
                }
                _p4CoinCount.text = "  " + tmpCount;
                break;

            default:
                return;
        }
    }

    public void IncreaseTrophyCount(short playerNumber)
    {
        int tmpCount = 0;
        switch (playerNumber)
        {
            case 1:
                tmpCount = Int32.Parse(_p1TrophyCount.text);
                tmpCount++;
                if (tmpCount > _numUpperLimit)
                {
                    tmpCount = _numUpperLimit;
                }
                _p1TrophyCount.text = "  " + tmpCount;
                break;

            case 2:
                tmpCount = Int32.Parse(_p2TrophyCount.text);
                tmpCount++;
                if (tmpCount > _numUpperLimit)
                {
                    tmpCount = _numUpperLimit;
                }
                _p2TrophyCount.text = "  " + tmpCount;
                break;

            case 3:
                tmpCount = Int32.Parse(_p3TrophyCount.text);
                tmpCount++;
                if (tmpCount > _numUpperLimit)
                {
                    tmpCount = _numUpperLimit;
                }
                _p3TrophyCount.text = "  " + tmpCount;
                break;

            case 4:
                tmpCount = Int32.Parse(_p4TrophyCount.text);
                tmpCount++;
                if (tmpCount > _numUpperLimit)
                {
                    tmpCount = _numUpperLimit;
                }
                _p4TrophyCount.text = "  " + tmpCount;
                break;

            default:
                return;
        }
    }

    public void DecreaseCoinCount(short playerNumber)
    {
        int tmpCount = 0;
        switch (playerNumber)
        {
            case 1:
                tmpCount = Int32.Parse(_p1CoinCount.text);
                tmpCount--;
                if (tmpCount < _numLowerLimit)
                {
                    tmpCount = _numLowerLimit;
                }
                _p1CoinCount.text = "  " + tmpCount;
                break;

            case 2:
                tmpCount = Int32.Parse(_p2CoinCount.text);
                tmpCount--;
                if (tmpCount < _numLowerLimit)
                {
                    tmpCount = _numLowerLimit;
                }
                _p2CoinCount.text = "  " + tmpCount;
                break;

            case 3:
                tmpCount = Int32.Parse(_p3CoinCount.text);
                tmpCount--;
                if (tmpCount < _numLowerLimit)
                {
                    tmpCount = _numLowerLimit;
                }
                _p3CoinCount.text = "  " + tmpCount;
                break;

            case 4:
                tmpCount = Int32.Parse(_p4CoinCount.text);
                tmpCount--;
                if (tmpCount < _numLowerLimit)
                {
                    tmpCount = _numLowerLimit;
                }
                _p4CoinCount.text = "  " + tmpCount;
                break;

            default:
                return;
        }
    }

    public void DecreaseTrophyCount(short playerNumber)
    {
        int tmpCount = 0;
        switch (playerNumber)
        {
            case 1:
                tmpCount = Int32.Parse(_p1TrophyCount.text);
                tmpCount--;
                if (tmpCount < _numLowerLimit)
                {
                    tmpCount = _numLowerLimit;
                }
                _p1TrophyCount.text = "  " + tmpCount;
                break;

            case 2:
                tmpCount = Int32.Parse(_p2TrophyCount.text);
                tmpCount--;
                if (tmpCount < _numLowerLimit)
                {
                    tmpCount = _numLowerLimit;
                }
                _p2TrophyCount.text = "  " + tmpCount;
                break;

            case 3:
                tmpCount = Int32.Parse(_p3TrophyCount.text);
                tmpCount--;
                if (tmpCount < _numLowerLimit)
                {
                    tmpCount = _numLowerLimit;
                }
                _p3TrophyCount.text = "  " + tmpCount;
                break;

            case 4:
                tmpCount = Int32.Parse(_p4TrophyCount.text);
                tmpCount--;
                if (tmpCount < _numLowerLimit)
                {
                    tmpCount = _numLowerLimit;
                }
                _p4TrophyCount.text = "  " + tmpCount;
                break;

            default:
                return;
        }
    }

    public void SetCoinCount(short playerNumber, int coins)
    {
        switch (playerNumber)
        {
            case 1:
                _p1CoinCount.text = "  " + coins;
                break;

            case 2:
                _p2CoinCount.text = "  " + coins;
                break;

            case 3:
                _p3CoinCount.text = "  " + coins;
                break;

            case 4:
                _p4CoinCount.text = "  " + coins;
                break;

            default:
                return;
        }
    }

    public void SetTrophyCount(short playerNumber, int trophies)
    {
        switch (playerNumber)
        {
            case 1:
                _p1TrophyCount.text = "  " + trophies;
                break;

            case 2:
                _p2TrophyCount.text = "  " + trophies;
                break;

            case 3:
                _p3TrophyCount.text = "  " + trophies;
                break;

            case 4:
                _p4TrophyCount.text = "  " + trophies;
                break;

            default:
                return;
        }
    }
}
