using UnityEngine;

[CreateAssetMenu(menuName = "Configuration/Bullet Element/Destroy On Hit")]
public class DestroyOnHitElement : BaseBulletElement
{
    public override void OnHit(IBullet bullet, GameObject player)
    {
        base.OnHit(bullet, player);

        bullet.Despawn();
    }
}
