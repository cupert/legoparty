using UnityEngine;

[CreateAssetMenu(menuName = "Configuration/Emitter/Sequentially From Line")]
public class SequenceFromLineEmitter : BaseEmissionProfile
{
    [field: SerializeField]
    public Vector3 Start { get; private set; }

    [field: SerializeField]
    public Vector3 End { get; private set; }

    [field: SerializeField]
    public Vector3 EmissionDirection { get; private set; }

    public override void Emit(Vector3 originPosition, Quaternion originRotation, IBullet bullet, int iteration = 0, int totalIterations = 1)
    {
        float ratio = iteration / (float)totalIterations;
        Vector3 position = Vector3.Lerp(originPosition + Start, originPosition + End, ratio);

        bullet.Position = position;
        bullet.Rotation = Quaternion.LookRotation(EmissionDirection);
    }
}
