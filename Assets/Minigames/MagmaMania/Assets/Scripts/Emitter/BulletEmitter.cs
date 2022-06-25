using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BulletEmitter : MonoBehaviour
{
    private ICollection<WaveProfile> waves;

    [SerializeField]
    private int initialPoolCapacity;

    [SerializeField]
    private int maxPoolCapacity;

    private Coroutine playlistCoroutine;
    private List<Coroutine> waveCoroutines;
    private List<Coroutine> patternCoroutines;

    private Dictionary<GameObject, IObjectPool<Bullet>> bulletPool;

    public void SetWaves(ICollection<WaveProfile> waves)
    {
        this.waves = waves;

        bulletPool = new Dictionary<GameObject, IObjectPool<Bullet>>();
        waveCoroutines = new List<Coroutine>();
        patternCoroutines = new List<Coroutine>();

        SetupBulletPools();
    }

    public void Play(PlaylistProfile playlist)
    {
        if (playlistCoroutine == null)
        {
            playlistCoroutine = StartCoroutine(PlayPlaylist(playlist));
        }
        else
        {
            Debug.LogWarning($"Emitter {name} is already playing back playlist {playlist.name}. Stop it first to play another one.");
        }
    }

    public void PlaySingleWave(WaveProfile wave)
    {
        if (waves.Contains(wave))
        {
            waveCoroutines.Add(StartCoroutine(PlayWave(wave)));
        }
        else
        {
            Debug.LogError($"Cannot play wave {wave.name} on emitter {name} because it has not been added to this emitter's available waves.");
        }
    }

    public void Stop()
    {
        if (playlistCoroutine != null)
            StopCoroutine(playlistCoroutine);

        foreach (Coroutine coroutine in patternCoroutines)
        {
            StopCoroutine(coroutine);
        }

        foreach (Coroutine coroutine in waveCoroutines)
        {
            StopCoroutine(coroutine);
        }

        playlistCoroutine = null;
    }

    public Bullet TakeBullet(GameObject prefab)
    {
        return bulletPool[prefab].Get();
    }

    public void ReturnBullet(Bullet bullet)
    {
        bulletPool[bullet.Profile.BulletPrefab].Release(bullet);
    }

    private IEnumerator PlayPlaylist(PlaylistProfile playlist)
    {
        int loopIndex = playlist.LoopToIndex < 0 ? playlist.Entries.Count + playlist.LoopToIndex : playlist.LoopToIndex;
        if (loopIndex >= playlist.Entries.Count) Debug.LogWarning($"Invalid loop index {loopIndex} in playlist {playlist.name}.");

        for (int i = 0; i < playlist.Entries.Count; i++)
        {
            PlaylistEntry entry = playlist.Entries[i];

            switch (entry.Action)
            {
                case PlaylistEntryType.WAIT:
                    yield return new WaitForSeconds(entry.Delay);
                    break;
                case PlaylistEntryType.PLAY:
                    PlaySingleWave(entry.Wave);
                    if (entry.WaitForCompletion) yield return new WaitForSeconds(entry.Wave.GetTotalDuration());
                    break;
                case PlaylistEntryType.PLAY_RANDOM:
                    WaveProfile wave = entry.Options[Random.Range(0, entry.Options.Count)];
                    PlaySingleWave(wave);
                    if (entry.WaitForCompletion) yield return new WaitForSeconds(wave.GetTotalDuration());
                    break;
            }

            if (i == playlist.Entries.Count - 1) i = loopIndex - 1;
        }

        playlistCoroutine = null;
    }

    private IEnumerator PlayWave(WaveProfile wave)
    {
        if (wave.StartDelay > 0) yield return new WaitForSeconds(wave.StartDelay);

        if (wave.PatternPlayback == WaveProfile.PatternPlaybackMode.PARALLEL)
        {
            float patternDuration = 0;
            foreach (PatternProfile pattern in wave.Patterns)
            {
                patternDuration = Mathf.Max(patternDuration, pattern.RepeatDelay * (pattern.Repetitions + 1));
                patternCoroutines.Add(StartCoroutine(PlayPattern(pattern)));
            }

            yield return new WaitForSeconds(patternDuration);
        }
        else
        {
            for (int i = 0; i < wave.Patterns.Count; i++)
            {
                PatternProfile pattern = wave.Patterns[i];
                patternCoroutines.Add(StartCoroutine(PlayPattern(pattern)));
                yield return new WaitForSeconds(pattern.RepeatDelay * (pattern.Repetitions + 1));
            }
        }

        if (wave.EndDelay > 0) yield return new WaitForSeconds(wave.EndDelay);
    }

    private IEnumerator PlayPattern(PatternProfile pattern)
    {
        for (int i = 0; i < pattern.Repetitions; i++)
        {
            int index = i;
            if (pattern.Reverse) index = pattern.Repetitions - 1 - i;

            if (pattern.Repetitions > 0 && i > 0) yield return new WaitForSeconds(pattern.RepeatDelay);

            Bullet bullet = TakeBullet(pattern.Bullet.BulletPrefab);
            pattern.Emission.Emit(transform.position, transform.rotation, bullet, index, pattern.Repetitions - 1);
            bullet.Spawn(pattern.Bullet);
        }
    }

    private void OnReturnToPool(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);

        bullet.transform.position = transform.position;
        bullet.transform.rotation = transform.rotation;
        bullet.transform.localScale = Vector3.one;
    }

    private void OnTakeFromPool(Bullet bullet)
    {
        bullet.gameObject.SetActive(true);
    }

    private void OnDestroyFromPool(Bullet bullet)
    {

    }

    private void SetupBulletPools()
    {
        foreach (WaveProfile wave in waves)
        {
            foreach (PatternProfile pattern in wave.Patterns)
            {
                if (!bulletPool.ContainsKey(pattern.Bullet.BulletPrefab))
                {
                    bulletPool.Add(pattern.Bullet.BulletPrefab, new ObjectPool<Bullet>(() =>
                    {
                        GameObject bulletObj = Instantiate(pattern.Bullet.BulletPrefab);
                        Bullet bullet = bulletObj.GetComponent<Bullet>();
                        bullet.Setup(this);
                        return bullet;
                    }
                    , OnTakeFromPool, OnReturnToPool, OnDestroyFromPool, true, initialPoolCapacity, maxPoolCapacity));
                }
            }
        }
    }

    #region Debug UI
    //private void OnGUI()
    //{
    //    if (waves != null && bulletPool != null)
    //    {
    //        if (GUILayout.Button("Play all in sequence"))
    //        {
    //            PlayAll();
    //        }

    //        if (GUILayout.Button("Play one randomly"))
    //        {
    //            PlayWave(waves[UnityEngine.Random.Range(0, waves.Count)]);
    //        }

    //        if (GUILayout.Button("Stop all"))
    //        {
    //            Stop();
    //        }

    //        GUILayout.Label("Play wave");
    //        GUILayout.BeginHorizontal();
    //        foreach (WaveProfile wave in waves)
    //        {
    //            if (GUILayout.Button(wave.name))
    //            {
    //                PlayWave(wave);
    //            }
    //        }
    //        GUILayout.EndHorizontal();

    //        GUILayout.Label("Pool stats:");
    //        foreach (KeyValuePair<GameObject, IObjectPool<Bullet>> entry in bulletPool)
    //        {
    //            GUILayout.Label($"{entry.Key.name} in pool: {entry.Value.CountInactive}");
    //        }
    //    }
    //    else
    //    {
    //        GUILayout.Label("The bullet pool is empty.");
    //    }
    //}
    #endregion
}
