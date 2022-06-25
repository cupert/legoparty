using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configuration/Playlist")]
public class PlaylistProfile : ScriptableObject
{
    [field: SerializeField]
    public List<PlaylistEntry> Entries { get; private set; }

    [field: SerializeField]
    public int LoopToIndex { get; private set; }
}

[Serializable]
public class PlaylistEntry
{
    public PlaylistEntryType Action;
    public float Delay;
    public WaveProfile Wave;
    public List<WaveProfile> Options;
    public bool WaitForCompletion;
}

public enum PlaylistEntryType { WAIT, PLAY, PLAY_RANDOM }
