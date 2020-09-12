using UnityEngine;

namespace AsteroidsClone
{
    [CreateAssetMenu(fileName = "ShipData")]
    public sealed class ShipData : Data
    {
        [Header("Settings")]
        [SerializeField] private PolygonShape shape;
        [SerializeField] private float angularSpeed;
        [SerializeField] private float stopSpeed;
        [SerializeField] private float speed;
        [SerializeField, Range(0, 1)] private float inertia;

        public PolygonShape Shape => shape;
        public float AngularSpeed => angularSpeed;
        public float StopSpeed => stopSpeed;
        public float Speed => speed;
        public float Inertia => inertia;
    }
}
