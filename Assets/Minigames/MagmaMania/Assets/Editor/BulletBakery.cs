using System.Collections.Generic;
using UnityEngine;

public class BulletBakery
{
    public static IEnumerable<BakedBullet> Bake(WaveProfile wave, WaveProfile.PatternPlaybackMode mode)
    {
        List<BakedBullet> baked = new List<BakedBullet>();

        float waveTime = 0;
        foreach (PatternProfile pattern in wave.Patterns)
        {
            float patternTime = 0;
            for (int i = 0; i < pattern.Repetitions; i++)
            {
                int index = i;
                if (pattern.Reverse) index = pattern.Repetitions - 1 - i;

                EditorBullet bullet = new EditorBullet();
                pattern.Emission.Emit(Vector3.zero, Quaternion.identity, bullet, index, pattern.Repetitions - 1);
                bullet.Spawn(pattern.Bullet);

                baked.Add(new BakedBullet(waveTime + patternTime, bullet.Position, bullet.Rotation, bullet.Velocity, bullet.AngularVelocity));

                if (pattern.Repetitions > 0) patternTime += pattern.RepeatDelay;
            }

            if (mode == WaveProfile.PatternPlaybackMode.SEQUENTIAL)
            {
                waveTime = pattern.RepeatDelay * pattern.Repetitions;
            }
        }

        return baked;
    }
}

public class BakedBullet
{
    private float spawnTime;

    private Vector3 spawnPosition;
    private Quaternion spawnRotation;

    private Vector3 velocity;
    private Vector3 angularVelocity;

    public BakedBullet(float spawnTime, Vector3 spawnPosition, Quaternion spawnRotation, Vector3 velocity, Vector3 angularVelocity)
    {
        this.spawnTime = spawnTime;
        this.spawnPosition = spawnPosition;
        this.spawnRotation = spawnRotation;
        this.velocity = velocity;
        this.angularVelocity = angularVelocity;
    }

    public bool Exists(float time)
    {
        return time >= spawnTime;
    }

    public Vector3 GetPosition(float time)
    {
        float timeSinceSpawn = time - spawnTime;
        return spawnPosition + velocity * timeSinceSpawn;
    }

    public Quaternion GetRotation(float time)
    {
        float timeSinceSpawn = time - spawnTime;
        Vector3 rotation = spawnRotation.eulerAngles + (angularVelocity * timeSinceSpawn);
        return Quaternion.Euler(rotation);
    }
}
