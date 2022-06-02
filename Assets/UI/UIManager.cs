using System;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private int _turnCount = 1;
    [SerializeField]
    private Text _scoreText;

    public static UIManager UIManagerRef;

    private void Awake()
    {
        //set static reference to script for other classes to use
        UIManagerRef = this;
    }
    
    void Start()
    {
        _scoreText.text = $"Turn {_turnCount}";
    }

    public void IncreaseTurnCount()
    {
        _turnCount++;
        _scoreText.text = $"Turn {_turnCount}";
    }
}
