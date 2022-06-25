using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configuration/Bullet Profile")]
public class BulletProfile : ScriptableObject
{
    [field: SerializeField]
    public GameObject BulletPrefab { get; private set; }

    [field: SerializeField]
    public List<BaseBulletElement> BulletElements;
}