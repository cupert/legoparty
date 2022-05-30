using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseField : MonoBehaviour, IField
{
    public FieldTypeEnum FieldType { get; set; } = FieldTypeEnum.BaseField;
    public bool IsTrophyField = false;
    public void InteractWithPlayer(PlayerInfo player)
    {
        if (IsTrophyField)
        {
            player.trophies++;
        }
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
