using UnityEngine;

[CreateAssetMenu(menuName = "Configuration/Emitter/Sequentially From Circle")]
public class SequenceFromCircleEmitter : BaseEmissionProfile
{
    [field: SerializeField]
    public Vector3 Center { get; private set; }

    [field: SerializeField]
    public float Radius { get; private set; }

    [field: SerializeField]
    public float StartAngle { get; private set; }

    public override void Emit(Vector3 originPosition, Quaternion originRotation, IBullet bullet, int iteration = 0, int totalIterations = 1)
    {
        float stepAngle = 2 * Mathf.PI / (totalIterations + 1);
        float startAngleRad = Mathf.Deg2Rad * StartAngle;
        Vector3 direction = new Vector3(Mathf.Cos(stepAngle * iteration + startAngleRad), 0, Mathf.Sin(stepAngle * iteration + startAngleRad));

        bullet.Position = originPosition + Center + direction * Radius;
        bullet.Rotation = Quaternion.LookRotation(direction);
    }
}
