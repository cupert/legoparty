using UnityEngine;

public interface IBullet
{
    public BulletProfile Profile { get; set; }
    public Vector3 Position { set; get; }
    public Quaternion Rotation { set; get; }
    public Vector3 Scale { set; get; }
    public Vector3 Velocity { set; get; }
    public Vector3 AngularVelocity { set; get; }
    public Material Material { set; get; }
    public void Spawn(BulletProfile profile);
    public void Despawn();
}
