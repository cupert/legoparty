using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class TimerController : MonoBehaviour
{
    [SerializeField]
    private GameStartedEvent gameStartedEvent;

    [SerializeField]
    private GameEndedEvent gameEndedEvent;

    [SerializeField]
    private ScriptableObjectEvent timerEndedEvent;

    [SerializeField]
    private int timeLimit;

    [SerializeField]
    private TextMeshProUGUI timerText;

    private int timer;
    private Coroutine timerCoroutine;

    [SerializeField] EmissionControl emissionControl;

    private void Start()
    {
        timer = timeLimit;
        gameStartedEvent.Add(OnGameStart);
        gameEndedEvent.Add(OnGameEnd);
    }

    private void OnGameStart(GameStartedEventArgs args)
    {
        timerCoroutine = StartCoroutine(Timer());
    }

    private void OnGameEnd(GameEndedEventArgs args)
    {
        StopCoroutine(timerCoroutine);
        gameObject.SetActive(false);
    }

    private IEnumerator Timer()
    {
        while (true)
        {
            timerText.text = $"{timer}";

            yield return new WaitForSeconds(1);

            if (timer > 0)
            {
                timer--;
                emissionControl.ChangeEmissionValue();
            }
            else
            {
                timerEndedEvent.Raise();
                break;
            }
        }
    }
}
