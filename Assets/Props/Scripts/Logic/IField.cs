using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IField
{
    public FieldTypeEnum FieldType { get; set; }
    public void InteractWithPlayer(PlayerInfo player);
}
