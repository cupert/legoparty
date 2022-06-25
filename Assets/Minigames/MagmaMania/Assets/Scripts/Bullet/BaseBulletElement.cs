using UnityEngine;

public abstract class BaseBulletElement : ScriptableObject
{
    public virtual void OnSpawn(IBullet bullet) { }
    //public virtual void OnUpdate(Bullet bullet) { }
    //public virtual void OnFixedUpdate(Bullet bullet) { }
    public virtual void OnHit(IBullet bullet, GameObject player) { }
}
