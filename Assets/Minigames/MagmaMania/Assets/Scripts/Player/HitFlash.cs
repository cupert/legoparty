using UnityEngine;

public class HitFlash : MonoBehaviour
{
    [SerializeField] float blinkIntensity;
    [SerializeField] float blinkDuration;
    float blinkTimer;
    [SerializeField] SkinnedMeshRenderer skinnedMeshRenderer;

    private void Start()
    {
        if (skinnedMeshRenderer == null)
        {
            skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        }
    }

    private void Update()
    {
        blinkTimer -= Time.deltaTime;
        float lerp = Mathf.Clamp01(blinkTimer / blinkDuration);
        float intensity = (lerp * blinkIntensity) + 1.0f;

        skinnedMeshRenderer.material.color = Color.white * intensity;
    }

    public void SetBlinkTimer()
    {
        blinkTimer = blinkDuration;
    }
}
