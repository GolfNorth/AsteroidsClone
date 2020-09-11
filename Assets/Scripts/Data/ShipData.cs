using UnityEngine;

namespace AsteroidsClone
{
    [CreateAssetMenu(fileName = "ShipData")]
    public sealed class ShipData : Data
    {
        [Header("Settings")]
        [SerializeField] private PolygonShape shape;
        [SerializeField] private float angularSpeed;
        [SerializeField] private float speed;

        public PolygonShape Shape => shape;
        public float AngularSpeed => angularSpeed;
        public float Speed => speed;
    }
}
