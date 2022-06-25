using UnityEngine;

public class EditorBullet : IBullet
{
    public BulletProfile Profile { get; set; }
    public Vector3 Position { get; set; }
    public Quaternion Rotation { get; set; }
    public Vector3 Scale { get; set; }
    public Vector3 Velocity { get; set; }
    public Vector3 AngularVelocity { get; set; }
    public Material Material { get; set; }

    public void Despawn() { }
    public void Spawn(BulletProfile profile)
    {
        this.Profile = profile;

        for (int i = 0; i < Profile.BulletElements.Count; i++)
        {
            Profile.BulletElements[i].OnSpawn(this);
        }
    }
}
