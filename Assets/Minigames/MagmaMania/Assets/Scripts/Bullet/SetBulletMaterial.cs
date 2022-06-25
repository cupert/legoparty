using UnityEngine;

[CreateAssetMenu(menuName = "Configuration/Bullet Element/Color Bullet")]
public class SetBulletMaterial : BaseBulletElement
{
    [field: SerializeField]
    public Material Material { get; private set; }

    public override void OnSpawn(IBullet bullet)
    {
        base.OnSpawn(bullet);

        bullet.Material = Material;
    }
}
