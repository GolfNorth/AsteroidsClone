using UnityEngine;

namespace AsteroidsClone
{
    [CreateAssetMenu(fileName = "UfoData")]
    public sealed class UfoData : Data
    {
        [Header("Settings")]
        [SerializeField] private PolygonShape shape;
        [SerializeField] private float speed;

        public PolygonShape Shape => shape;
        public float Speed => speed;
    }
}
