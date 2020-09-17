using UnityEngine;

namespace AsteroidsView
{
    public static class VectorExtensions
    {
        #region Methods

        public static Vector2 ToUnity(this System.Numerics.Vector2 vector)
        {
            return new Vector2(vector.X, vector.Y);
        }

        #endregion
    }
}