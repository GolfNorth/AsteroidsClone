using System;
using UnityEngine;

namespace AsteroidsClone
{
    [Serializable]
    public struct PolygonShape
    {
        public Vector2[] Points;
        public Vector2 Center;
    }
}