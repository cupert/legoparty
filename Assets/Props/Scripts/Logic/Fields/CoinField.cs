using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinField : MonoBehaviour, IField
{
    public FieldTypeEnum FieldType { get; set; } = FieldTypeEnum.CoinField;

    public void InteractWithPlayer(PlayerInfo player)
    {
        player.coins += 3;
        Debug.Log("End of turn");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
