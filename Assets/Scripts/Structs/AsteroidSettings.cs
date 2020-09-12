using System;

namespace AsteroidsClone
{
    [Serializable]
    public struct AsteroidSettings
    {
        public AsteroidSize AsteroidSize;
        public float Radius;
        public float MinSpeed;
        public float MaxSpeed;
    }
}