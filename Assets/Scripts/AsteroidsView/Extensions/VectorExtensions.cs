using AsteroidsCore;

namespace AsteroidsView
{
    public static class VectorExtensions
    {
        public static UnityEngine.Vector2 ToUnity(this Vector2 vector)
        {
            return new UnityEngine.Vector2(vector.x, vector.y);
        }
    }
}