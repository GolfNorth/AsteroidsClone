using UnityEngine;

namespace AsteroidsClone
{
    [CreateAssetMenu(fileName = "LaserData")]
    public sealed class LaserData : Data
    {
        [Header("Settings")]
        [SerializeField] private float range;

        public float Range => range;
    }
}
