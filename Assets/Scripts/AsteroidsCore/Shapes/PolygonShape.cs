using System;
using System.Numerics;

namespace AsteroidsCore
{
    [Serializable]
    public struct PolygonShape
    {
        public Vector2[] Points;
        public Vector2 Center;
    }
}