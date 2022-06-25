using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object Event/Player")]
public class PlayerEvent : ScriptableObjectEvent<PlayerEventArgs> { }

public struct PlayerEventArgs
{
    public PlayerProfile Player;
}