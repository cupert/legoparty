using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private int maxPlayers;

    [SerializeField]
    private List<PlayerProfile> players;

    [SerializeField]
    private PlayerEvent playerJoinEvent;

    [SerializeField]
    private GameStartedEvent gameStartEvent;

    [SerializeField]
    private float spawnRadius;

    private int nextPlayerIndex;

    private bool gameStarted;

    private void Start()
    {
        gameStarted = false;

        gameStartEvent.Add((args) => { gameStarted = true; });
    }

    private void OnPlayerJoined(PlayerInput playerInput)
    {
        GameObject player = playerInput.gameObject;

        if (nextPlayerIndex == maxPlayers || gameStarted)
        {
            Destroy(player);
            return;
        }

        PlayerController controller = player.GetComponent<PlayerController>();

        player.transform.position = GetPlayerStartingPoint(nextPlayerIndex, players.Count);
        controller.Setup(players[nextPlayerIndex]);

        playerJoinEvent.Raise(new PlayerEventArgs() { Player = players[nextPlayerIndex] });

        nextPlayerIndex++;
    }

    private Vector3 GetPlayerStartingPoint(int player, int totalPlayers)
    {
        float stepAngle = 2 * Mathf.PI / totalPlayers;
        Vector3 direction = new Vector3(Mathf.Cos(stepAngle * player), 0, Mathf.Sin(stepAngle * player));
        return transform.position + direction * spawnRadius;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        // Visualize player spawn points
        if (players.Count > 0)
        {
            for (int i = 0; i < players.Count; i++)
            {
                Gizmos.DrawSphere(GetPlayerStartingPoint(i, players.Count), 0.25f);
            }
        }
    }
}
