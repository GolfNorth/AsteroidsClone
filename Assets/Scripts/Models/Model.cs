using UnityEngine;

namespace AsteroidsClone
{
    public abstract class Model
    {
        protected ViewMode viewMode;
        protected Vector3 position;
        protected Quaternion rotation;

        public ViewMode ViewMode { get => viewMode; set => viewMode = value; }
        public Vector3 Position { get => position; set => position = value; }
        public Quaternion Rotation { get => rotation; set => rotation = value; }
    }
}
