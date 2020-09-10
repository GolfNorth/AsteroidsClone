using System;
using UnityEngine;

namespace AsteroidsClone
{
    [Serializable]
    public struct AsteroidSettings
    {
        public AsteroidSize AsteroidSize;
        public float MinSpeed;
        public float MaxSpeed;
    }
}
