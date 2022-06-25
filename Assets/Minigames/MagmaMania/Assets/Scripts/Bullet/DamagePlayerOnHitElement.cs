using UnityEngine;

[CreateAssetMenu(menuName = "Configuration/Bullet Element/Damage Player On Hit")]
public class DamagePlayerOnHitElement : BaseBulletElement
{
    [field: SerializeField]
    public float Amount { get; private set; }

    public override void OnHit(IBullet bullet, GameObject player)
    {
        base.OnHit(bullet, player);

        PlayerController controller = player.GetComponent<PlayerController>();
        controller.Damage(Amount, player.transform.position - bullet.Position);
    }
}
