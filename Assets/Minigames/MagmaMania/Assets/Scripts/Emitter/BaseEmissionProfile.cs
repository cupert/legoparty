using UnityEngine;

public abstract class BaseEmissionProfile : ScriptableObject
{
    public abstract void Emit(Vector3 originPosition, Quaternion originRotation, IBullet bullet, int iteration = 0, int totalIterations = 1);
}
