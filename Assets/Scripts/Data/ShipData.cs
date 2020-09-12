using UnityEngine;

namespace AsteroidsClone
{
    [CreateAssetMenu(fileName = "ShipData")]
    public sealed class ShipData : Data
    {
        #region Fields

        [Header("Settings")]
        [SerializeField] private PolygonShape shape;
        [SerializeField] private float angularSpeed;
        [SerializeField] private float stopSpeed;
        [SerializeField] private float speed;
        [SerializeField] [Range(0, 1)] private float inertia;

        #endregion

        #region Properties

        public PolygonShape Shape => shape;
        public float AngularSpeed => angularSpeed;
        public float StopSpeed => stopSpeed;
        public float Speed => speed;
        public float Inertia => inertia;

        #endregion
    }
}