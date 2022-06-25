using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Image foreground = null;
    [SerializeField] Image background = null;

    [SerializeField] float lerpTimer;
    [SerializeField] float chipSpeed = 2f;

    PlayerController playerController = null;

    private void Start()
    {
        playerController = GetComponentInParent<PlayerController>();
    }

    void Update()
    {
        UpdateHealthUI();
    }

    public void ResetLerpTimer()
    {
        lerpTimer = 0f;
    }

    public void UpdateHealthUI()
    {
        float fillFront = foreground.fillAmount;
        float fillBack = background.fillAmount;
        float healthFraction = playerController.GetHealthFraction();

        if (fillBack > healthFraction)
        {
            foreground.fillAmount = healthFraction;
            // background.color = Color.white;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete; // smooth animation
            background.fillAmount = Mathf.Lerp(fillBack, healthFraction, percentComplete);
        }

        if (fillFront < healthFraction)
        {
            // background.color = Color.white;
            background.fillAmount = healthFraction;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete; // smooth animation
            foreground.fillAmount = Mathf.Lerp(fillFront, background.fillAmount, percentComplete);
        }
    }
}