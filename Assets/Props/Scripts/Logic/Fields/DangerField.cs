using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerField : MonoBehaviour, IField
{
    public FieldTypeEnum FieldType { get; set; } = FieldTypeEnum.DangerField;

    public void InteractWithPlayer(PlayerInfo player)
    {
        player.coins -= 2;
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
