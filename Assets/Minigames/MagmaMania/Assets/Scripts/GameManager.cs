using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private PlaylistProfile playlist;

    [SerializeField]
    private PlaylistProfile suddenDeathPlaylist;

    [SerializeField]
    private BulletEmitter emitter;

    [SerializeField]
    private GameStartedEvent gameStarted;

    [SerializeField]
    private GameEndedEvent gameEnded;

    [SerializeField]
    private ScriptableObjectEvent timerEnded;

    [SerializeField]
    private PlayerEvent playerDeathEvent;

    [SerializeField]
    private Camera mainCamera;

    [SerializeField]
    private AudioClip backgroundWindSound;

    [SerializeField]
    private AudioClip backgroundMusic;

    [SerializeField]
    private AudioClip winSound;

    private int totalPlayers;
    private List<PlayerProfile> livingPlayers;
    private List<PlayerProfile> ranking;

    private void Start()
    {
        livingPlayers = new List<PlayerProfile>();
        ranking = new List<PlayerProfile>();

        gameStarted.Add(OnStartGame);
        gameEnded.Add(OnEndGame);
        timerEnded.Add(OnTimerEnd);
        playerDeathEvent.Add(OnPlayerDied);
        mainCamera.GetComponent<AudioSource>().clip = backgroundWindSound;
        mainCamera.GetComponent<AudioSource>().Play();
    }

    private void OnStartGame(GameStartedEventArgs args)
    {
        HashSet<WaveProfile> distinctWaves = FindDistinctWaves(playlist);
        distinctWaves.UnionWith(FindDistinctWaves(suddenDeathPlaylist));
        emitter.SetWaves(distinctWaves);

        livingPlayers = new List<PlayerProfile>(args.Players);
        totalPlayers = livingPlayers.Count;

        if (playlist.Entries.Count > 0)
        {
            emitter.Play(playlist);
        }

        mainCamera.GetComponent<AudioSource>().clip = backgroundMusic;
        mainCamera.GetComponent<AudioSource>().Play();
    }

    private void OnTimerEnd()
    {
        emitter.Stop();
        emitter.Play(suddenDeathPlaylist);
    }

    private void OnEndGame(GameEndedEventArgs args)
    {
        emitter.Stop();
        mainCamera.GetComponent<AudioSource>().clip = winSound;
        mainCamera.GetComponent<AudioSource>().loop = false;
        mainCamera.GetComponent<AudioSource>().Play();
    }

    private void OnPlayerDied(PlayerEventArgs args)
    {
        if (ranking.Contains(args.Player)) return;

        livingPlayers.Remove(args.Player);
        ranking.Add(args.Player);

        if (ranking.Count == totalPlayers - 1)
        {
            ranking.Add(livingPlayers[0]);
            ranking.Reverse();

            gameEnded.Raise(new GameEndedEventArgs() { Ranking = ranking });
        }
    }

    private HashSet<WaveProfile> FindDistinctWaves(PlaylistProfile playlist)
    {
        HashSet<WaveProfile> distinctWaves = new HashSet<WaveProfile>();
        foreach (PlaylistEntry entry in playlist.Entries)
        {
            switch (entry.Action)
            {
                case PlaylistEntryType.PLAY:
                    distinctWaves.Add(entry.Wave);
                    break;
                case PlaylistEntryType.PLAY_RANDOM:
                    foreach (WaveProfile wave in entry.Options)
                    {
                        distinctWaves.Add(wave);
                    }
                    break;
            }
        }

        return distinctWaves;
    }
}