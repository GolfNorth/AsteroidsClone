using UnityEngine;

namespace AsteroidsClone
{
    [CreateAssetMenu(fileName = "BulletData")]
    public sealed class BulletData : Data
    {
        [Header("Settings")]
        [SerializeField] private float range;
        [SerializeField] private float speed;
        [SerializeField] private float rapidity;

        public float Range => range;
        public float Speed => speed;
        public float Rapidity => rapidity;
    }
}
