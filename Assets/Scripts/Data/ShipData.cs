using UnityEngine;

namespace AsteroidsClone
{
    [CreateAssetMenu(fileName = "ShipData")]
    public sealed class ShipData : Data
    {
        [Header("Settings")]
        [SerializeField] private PolygonShape shape;
        [SerializeField] private float acceleration;
        [SerializeField] private float angularSpeed;
        [SerializeField] private float maximumSpeed;

        public PolygonShape Shape => shape;
        public float Acceleration => acceleration;
        public float AngularSpeed => angularSpeed;
        public float MaximumSpeed => maximumSpeed;
    }
}
