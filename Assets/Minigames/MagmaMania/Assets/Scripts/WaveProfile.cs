using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configuration/Wave Profile")]
public class WaveProfile : ScriptableObject
{
    [field: SerializeField]
    public float StartDelay { get; private set; }

    [field: SerializeField]
    public float EndDelay { get; private set; }

    [field: SerializeField]
    public PatternPlaybackMode PatternPlayback { get; private set; }

    [field: SerializeField]
    public List<PatternProfile> Patterns { get; private set; }

    [Serializable]
    public enum PatternPlaybackMode { SEQUENTIAL, PARALLEL }

    public float GetTotalDuration()
    {
        float duration = StartDelay + EndDelay;

        if (PatternPlayback == WaveProfile.PatternPlaybackMode.PARALLEL)
        {
            float patternDuration = 0;
            foreach (PatternProfile pattern in Patterns)
            {
                patternDuration = Mathf.Max(patternDuration, pattern.RepeatDelay * (pattern.Repetitions) + 1);
            }

            duration += patternDuration;
        }
        else
        {
            for (int i = 0; i < Patterns.Count; i++)
            {
                PatternProfile pattern = Patterns[i];
                duration += pattern.RepeatDelay * (pattern.Repetitions + 1);
            }
        }

        return duration;
    }
}