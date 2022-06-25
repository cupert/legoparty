using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndController : MonoBehaviour
{
    [SerializeField]
    private GameEndedEvent gameEndedEvent;

    [SerializeField]
    private PlayerEvent startPressedEvent;

    [SerializeField]
    private TextMeshProUGUI rankingsText;

    private bool gameEnded;

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }

    private void Start()
    {
        gameEnded = false;

        gameEndedEvent.Add(OnGameEnd);

        startPressedEvent.Add((args) =>
        {
            if (gameEnded) RestartGame();
        });

        gameObject.SetActive(false);
    }

    private void OnGameEnd(GameEndedEventArgs args)
    {
        gameEnded = true;

        gameObject.SetActive(true);

        StringBuilder rankingsTextBuilder = new StringBuilder();
        int i = 1;
        foreach (PlayerProfile player in args.Ranking)
        {
            rankingsTextBuilder.Append($"{i}. Player {player.PlayerNumber}\n");
            i++;
        }

        rankingsText.text = rankingsTextBuilder.ToString();
    }
}
