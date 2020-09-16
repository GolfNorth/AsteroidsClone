using System;

namespace AsteroidsCore
{
    [Serializable]
    public struct UfoData : IData
    {
        #region Fields

        public PolygonShape shape;

        public float spawnDelay;

        public float speed;

        #endregion
    }
}