using System.Collections;
using System.Collections.Generic;
using Unity.EditorCoroutines.Editor;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WaveProfile))]
public class WaveProfileEditor : Editor
{
    private const float uiWidth = 200;

    private enum PlaybackState { PLAY, PAUSE, STOPPED };
    private PlaybackState playbackState;

    private WaveProfile waveProfile;
    private EditorCoroutine playbackCoroutine;

    private float playbackPosition;
    private float maxSimulationTime;

    private IEnumerable<BakedBullet> bakedBullets;

    private void OnEnable()
    {
        waveProfile = (WaveProfile)target;
        bakedBullets = BulletBakery.Bake(waveProfile, waveProfile.PatternPlayback);
        maxSimulationTime = (waveProfile.GetTotalDuration() - waveProfile.StartDelay - waveProfile.EndDelay) * 2;

        playbackState = PlaybackState.PLAY;

        playbackCoroutine = EditorCoroutineUtility.StartCoroutine(PlayWave(), this);
        SceneView.duringSceneGui += OnSceneGUI;
    }

    private void OnDisable()
    {
        EditorCoroutineUtility.StopCoroutine(playbackCoroutine);
        SceneView.duringSceneGui -= OnSceneGUI;
    }

    private void OnSceneGUI(SceneView view)
    {
        if (playbackState != PlaybackState.STOPPED)
        {
            foreach (BakedBullet bullet in bakedBullets)
            {
                if (bullet.Exists(playbackPosition))
                {
                    DrawBullet(bullet.GetPosition(playbackPosition), bullet.GetRotation(playbackPosition), 0.5f);
                }
            }
        }

        Handles.color = Color.white;

        Handles.BeginGUI();

        GUILayout.Box("Preview", GUILayout.Width(uiWidth));

        GUILayout.BeginHorizontal();

        if (playbackState == PlaybackState.PLAY)
        {
            if (GUILayout.Button("Pause", GUILayout.Width(uiWidth / 3)))
            {
                playbackState = PlaybackState.PAUSE;
            }

            if (GUILayout.Button("Stop", GUILayout.Width(uiWidth / 3)))
            {
                playbackState = PlaybackState.STOPPED;
                playbackPosition = 0;
                Rebake();
            }

            if (GUILayout.Button("Restart", GUILayout.Width(uiWidth / 3)))
            {
                playbackPosition = 0;
                Rebake();
            }
        }
        else
        {
            if (GUILayout.Button("Play", GUILayout.Width(uiWidth)))
            {
                playbackState = PlaybackState.PLAY;
            }
        }

        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();

        playbackPosition = GUILayout.HorizontalSlider(playbackPosition, 0, maxSimulationTime, GUILayout.Width(uiWidth));

        GUILayout.EndHorizontal();

        Handles.EndGUI();
    }

    private void Rebake()
    {
        bakedBullets = BulletBakery.Bake(waveProfile, waveProfile.PatternPlayback);
    }

    private void DrawBullet(Vector3 position, Quaternion rotation, float radius)
    {
        Handles.color = Color.white;
        Handles.DrawWireDisc(position, rotation * Vector3.up, radius);
        Handles.DrawWireDisc(position, rotation * Vector3.forward, radius);
        Handles.DrawWireDisc(position, rotation * Vector3.right, radius);

        Handles.color = Color.blue;
        DrawArrow(position, rotation, radius * 2);
    }

    private void DrawArrow(Vector3 start, Quaternion rotation, float length)
    {
        Vector3 tip = start + rotation * Vector3.forward * length;
        Handles.DrawLine(start, tip);
    }

    private IEnumerator PlayWave()
    {
        while (true)
        {
            yield return new EditorWaitForSeconds(Time.fixedDeltaTime);

            if (playbackState == PlaybackState.PLAY)
            {
                playbackPosition += Time.fixedDeltaTime;
                if (playbackPosition > maxSimulationTime) playbackPosition = 0;

                SceneView.lastActiveSceneView.Repaint();
            }
        }
    }
}
