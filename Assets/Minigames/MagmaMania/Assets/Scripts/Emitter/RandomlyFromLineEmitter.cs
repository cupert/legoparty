using UnityEngine;

[CreateAssetMenu(menuName = "Configuration/Emitter/Randomly From Line")]
public class RandomlyFromLineEmitter : BaseEmissionProfile
{
    [field: SerializeField]
    public Vector3 Start { get; private set; }

    [field: SerializeField]
    public Vector3 End { get; private set; }

    [field: SerializeField]
    public Vector3 EmissionDirection { get; private set; }

    public override void Emit(Vector3 originPosition, Quaternion originRotation, IBullet bullet, int iteration = 1, int totalIterations = 1)
    {
        float ratio = Random.Range(0f, 1f);
        Vector3 position = Vector3.Lerp(originPosition + Start, originPosition + End, ratio);

        bullet.Position = position;
        bullet.Rotation = Quaternion.LookRotation(EmissionDirection);
    }
}
