using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object Event/Game Ended")]
public class GameEndedEvent : ScriptableObjectEvent<GameEndedEventArgs> { }

public struct GameEndedEventArgs
{
    public List<PlayerProfile> Ranking;
}
