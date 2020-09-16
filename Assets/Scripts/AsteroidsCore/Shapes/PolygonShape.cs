using System;

namespace AsteroidsCore
{
    [Serializable]
    public struct PolygonShape
    {
        public Vector2[] Points;
        public Vector2 Center;
    }
}