using UnityEngine;

[CreateAssetMenu(menuName = "Configuration/Player Profile")]
public class PlayerProfile : ScriptableObject
{
    [field: SerializeField]
    public int PlayerNumber { get; private set; }

    [field: SerializeField]
    public GameObject ModelPrefab { get; private set; }
}
