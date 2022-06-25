using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour, IBullet
{
    public BulletProfile Profile { get; set; }
    public Vector3 Position { get => transform.position; set => transform.position = value; }
    public Quaternion Rotation { get => transform.rotation; set => transform.rotation = value; }
    public Vector3 Scale { get => transform.localScale; set => transform.localScale = value; }
    public Vector3 Velocity { get => rigidbody.velocity; set => rigidbody.velocity = value; }
    public Vector3 AngularVelocity { get => rigidbody.angularVelocity; set => rigidbody.angularVelocity = value; }
    public Material Material { get => meshRenderer.material; set => meshRenderer.material = value; }

    private BulletEmitter emitter;
    private new Rigidbody rigidbody;
    private MeshRenderer meshRenderer;

    public void Setup(BulletEmitter emitter)
    {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        rigidbody = GetComponent<Rigidbody>();

        this.emitter = emitter;
    }

    public void Spawn(BulletProfile profile)
    {
        this.Profile = profile;

        OnSpawn();
    }

    public void Despawn()
    {
        emitter.ReturnBullet(this);
    }

    private void OnSpawn()
    {
        for (int i = 0; i < Profile.BulletElements.Count; i++)
        {
            Profile.BulletElements[i].OnSpawn(this);
        }
    }

    //private void Update()
    //{
    //    for (int i = 0; i < Profile.BulletElements.Count; i++)
    //    {
    //        Profile.BulletElements[i].OnUpdate(this);
    //    }
    //}

    //private void FixedUpdate()
    //{
    //    for (int i = 0; i < Profile.BulletElements.Count; i++)
    //    {
    //        Profile.BulletElements[i].OnFixedUpdate(this);
    //    }
    //}

    private void OnCollisionEnter(Collision collision)
    {
        Collider other = collision.collider;
        if (other.CompareTag(Tags.PLAYER))
        {
            for (int i = 0; i < Profile.BulletElements.Count; i++)
            {
                Profile.BulletElements[i].OnHit(this, other.gameObject);
            }
        }
    }
}
