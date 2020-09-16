using UnityEngine;

namespace AsteroidsView
{
    public static class VectorExtensions
    {
        #region Methods

        public static Vector2 ToUnity(this AsteroidsCore.Vector2 vector)
        {
            return new Vector2(vector.x, vector.y);
        }

        #endregion
    }
}