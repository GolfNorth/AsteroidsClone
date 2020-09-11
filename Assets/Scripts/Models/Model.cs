using UnityEngine;

namespace AsteroidsClone
{
    public abstract class Model
    {
        protected ViewMode viewMode;
        protected Vector2 position;
        protected Vector2 velocity;
        protected float angle;

        public ViewMode ViewMode { get => viewMode; set => viewMode = value; }
        public Vector2 Position { get => position; set => position = value; }
        public Vector2 Velocity { get => velocity; set => velocity = value; }
        public float Angle { get => angle; set => angle = value; }
    }
}
