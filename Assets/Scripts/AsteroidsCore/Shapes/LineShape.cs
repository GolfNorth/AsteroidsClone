using System;
using System.Numerics;

namespace AsteroidsCore
{
    [Serializable]
    public struct LineShape
    {
        public Vector2 PointA;
        public Vector2 PointB;
        public Vector2 Center;
    }
}