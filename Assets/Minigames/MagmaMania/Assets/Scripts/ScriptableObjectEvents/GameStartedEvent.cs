using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object Event/Game Started")]
public class GameStartedEvent : ScriptableObjectEvent<GameStartedEventArgs> { }

public struct GameStartedEventArgs
{
    public List<PlayerProfile> Players;
}
