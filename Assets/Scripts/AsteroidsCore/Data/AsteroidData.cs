using System;
using System.Collections.Generic;

namespace AsteroidsCore
{
    [Serializable]
    public struct AsteroidData : IData
    {
        #region Struct

        [Serializable]
        public struct AsteroidUnitData
        {
            public AsteroidSize asteroidSize;
            public float radius;
            public float minSpeed;
            public float maxSpeed;
        }

        #endregion

        #region Fields

        public float spawnDelay;
        public Dictionary<AsteroidSize, AsteroidUnitData> UnitData;

        #endregion
    }
}