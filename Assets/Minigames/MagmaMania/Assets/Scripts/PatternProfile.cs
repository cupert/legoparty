using UnityEngine;

[CreateAssetMenu(menuName = "Configuration/Pattern Profile")]
public class PatternProfile : ScriptableObject
{
    [field: SerializeField]
    public BulletProfile Bullet { get; private set; }

    [field: SerializeField]
    public BaseEmissionProfile Emission { get; private set; }

    [field: SerializeField]
    public int Repetitions { get; private set; } = 1;

    [field: SerializeField]
    public float RepeatDelay { get; private set; }

    [field: SerializeField]
    public bool Reverse { get; private set; }
}
