using UnityEngine;

[CreateAssetMenu(menuName = "Configuration/Bullet Element/Fly Forward")]
public class FlyForwardElement : BaseBulletElement
{
    [field: SerializeField]
    public float Speed { get; private set; }

    public override void OnSpawn(IBullet bullet)
    {
        base.OnSpawn(bullet);

        bullet.Velocity = bullet.Rotation * Vector3.forward * Speed;
    }
}
