using System;

namespace AsteroidsCore
{
    [Serializable]
    public struct ShipData : IData
    {
        #region Fields

        public PolygonShape shape;

        public float angularSpeed;

        public float stopSpeed;

        public float speed;

        public float inertia;

        #endregion
    }
}