using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LobbyController : MonoBehaviour
{
    [SerializeField]
    private int MinPlayers = 2;

    [SerializeField]
    private GameStartedEvent gameStartEvent;

    [SerializeField]
    private PlayerEvent joinEvent;

    [SerializeField]
    private PlayerEvent startPressedEvent;

    [SerializeField]
    private AudioClip countDownTick;

    [SerializeField]
    private AudioClip startGameSound;

    [SerializeField]
    private Camera mainCamera;

    [SerializeField]
    private TextMeshProUGUI countDownText;

    [SerializeField]
    private GameObject startGameButton;

    [SerializeField]
    private TextMeshProUGUI startGameText;

    [SerializeField]
    private TextMeshProUGUI controlsHintText;

    private int playerCount;
    private List<PlayerProfile> activePlayers;
    private AudioSource camAudioSource;

    private bool alreadyStarted;

    private void Start()
    {
        alreadyStarted = false;

        activePlayers = new List<PlayerProfile>();

        joinEvent.Add((args) =>
        {
            playerCount++;
            activePlayers.Add(args.Player);
        });

        startPressedEvent.Add((args) =>
        {
            if (args.Player.PlayerNumber == 1) StartGame();
        });

        camAudioSource = mainCamera.GetComponent<AudioSource>();
        countDownText.gameObject.SetActive(false);
    }

    public void StartGame()
    {
        if (alreadyStarted) return;

        if (playerCount >= MinPlayers)
        {
            alreadyStarted = true;
            StartCoroutine(StartCountDown());
        }
        else
        {
            Debug.Log($"Cannot start game with less than {MinPlayers} players.");
        }
    }

    private IEnumerator StartCountDown()
    {
        startGameButton.SetActive(false);
        startGameText.gameObject.SetActive(false);
        controlsHintText.gameObject.SetActive(false);

        countDownText.gameObject.SetActive(true);

        camAudioSource.PlayOneShot(countDownTick);
        countDownText.text = "3";
        yield return new WaitForSeconds(1);
        camAudioSource.PlayOneShot(countDownTick);
        countDownText.text = "2";
        yield return new WaitForSeconds(1);
        camAudioSource.PlayOneShot(countDownTick);
        countDownText.text = "1";
        yield return new WaitForSeconds(1);
        camAudioSource.PlayOneShot(startGameSound);
        countDownText.text = "GO!";
        yield return new WaitForSeconds(1);
        gameStartEvent.Raise(new GameStartedEventArgs() { Players = activePlayers });
        gameObject.SetActive(false);
    }
}
