using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsPanel : MonoBehaviour
{
    [SerializeField]
    private int targetPlayer;
    [SerializeField]
    private PlayerEvent playerJoinEvent;
    [SerializeField]
    private PlayerDamagedEvent playerLoseHpEvent;
    [SerializeField]
    private PlayerEvent playerDeathEvent;

    public Image HpBarImage;

    private void Start()
    {
        playerJoinEvent.Add(OnPlayerJoin);
        playerLoseHpEvent.Add(OnPlayerLoseHp);
        playerDeathEvent.Add(OnPlayerDied);

        gameObject.SetActive(false);
    }

    private void OnPlayerJoin(PlayerEventArgs args)
    {
        if (args.Player.PlayerNumber == targetPlayer) gameObject.SetActive(true);
    }

    private void OnPlayerLoseHp(PlayerDamagedEventArgs args)
    {
        if (args.Player.PlayerNumber == targetPlayer) HpBarImage.fillAmount = args.NewValue / 100;
    }

    private void OnPlayerDied(PlayerEventArgs args)
    {
        if (args.Player.PlayerNumber == targetPlayer)
        {
            HpBarImage.fillAmount = 1;
            HpBarImage.color = Color.red;
        }
    }
}
