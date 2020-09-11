using System;
using UnityEngine;

namespace AsteroidsClone
{
    public abstract class Model
    {
        public ViewMode ViewMode { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public float Angle { get; set; }
    }
}
