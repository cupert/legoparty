using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object Event/Player Damaged")]
public class PlayerDamagedEvent : ScriptableObjectEvent<PlayerDamagedEventArgs> { }

public struct PlayerDamagedEventArgs
{
    public PlayerProfile Player;
    public float NewValue;
}