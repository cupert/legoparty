using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseField : MonoBehaviour, IField
{
    public FieldTypeEnum FieldType { get; set; } = FieldTypeEnum.BaseField;
    public bool IsTrophyField { get; set; } = false;
    public void InteractWithPlayer(PlayerInfo player)
    {
        Debug.Log("End of turn");
    }
    public void GiveTrophy(PlayerInfo player)
    {
        player.trophies++;
        IsTrophyField = false;
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
