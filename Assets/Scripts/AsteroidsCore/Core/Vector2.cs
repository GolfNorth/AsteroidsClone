using System;

namespace AsteroidsCore
{
    [Serializable]
    public struct Vector2 : IEquatable<Vector2>
    {
        #region Private Fields

        private static readonly Vector2 zeroVector = new Vector2(0f, 0f);
        private static readonly Vector2 unitVector = new Vector2(1f, 1f);

        #endregion

        #region Public Fields
        
        public float x;
        
        public float y;

        #endregion

        #region Properties
        
        public static Vector2 Zero => zeroVector;

        public static Vector2 One => unitVector;

        #endregion

        #region Constructors
        
        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public Vector2(float value)
        {
            x = value;
            y = value;
        }

        #endregion

        #region Operators
        
        public static Vector2 operator -(Vector2 value)
        {
            value.x = -value.x;
            value.y = -value.y;
            
            return value;
        }
        
        public static Vector2 operator +(Vector2 value1, Vector2 value2)
        {
            value1.x += value2.x;
            value1.y += value2.y;
            
            return value1;
        }
        
        public static Vector2 operator -(Vector2 value1, Vector2 value2)
        {
            value1.x -= value2.x;
            value1.y -= value2.y;
            
            return value1;
        }
        
        public static Vector2 operator *(Vector2 value1, Vector2 value2)
        {
            value1.x *= value2.x;
            value1.y *= value2.y;
            
            return value1;
        }
        
        public static Vector2 operator *(Vector2 value, float scaleFactor)
        {
            value.x *= scaleFactor;
            value.y *= scaleFactor;
            
            return value;
        }
        
        public static Vector2 operator *(float scaleFactor, Vector2 value)
        {
            value.x *= scaleFactor;
            value.y *= scaleFactor;
            
            return value;
        }
        
        public static Vector2 operator /(Vector2 value1, Vector2 value2)
        {
            value1.x /= value2.x;
            value1.y /= value2.y;
            
            return value1;
        }
        
        public static Vector2 operator /(Vector2 value1, float divider)
        {
            var factor = 1 / divider;
            
            value1.x *= factor;
            value1.y *= factor;
            
            return value1;
        }
        
        public static bool operator ==(Vector2 value1, Vector2 value2)
        {
            return value1.x == value2.x && value1.y == value2.y;
        }
        
        public static bool operator !=(Vector2 value1, Vector2 value2)
        {
            return value1.x != value2.x || value1.y != value2.y;
        }

        #endregion

        #region Methods
        
        public static float Distance(Vector2 value1, Vector2 value2)
        {
            float v1 = value1.x - value2.x, v2 = value1.y - value2.y;
            
            return (float)Math.Sqrt((v1 * v1) + (v2 * v2));
        }
        
        public static void Distance(ref Vector2 value1, ref Vector2 value2, out float result)
        {
            float v1 = value1.x - value2.x, v2 = value1.y - value2.y;
            
            result = (float)Math.Sqrt((v1 * v1) + (v2 * v2));
        }
        
        public static float DistanceSquared(Vector2 value1, Vector2 value2)
        {
            float v1 = value1.x - value2.x, v2 = value1.y - value2.y;
            
            return (v1 * v1) + (v2 * v2);
        }
        
        public static void DistanceSquared(ref Vector2 value1, ref Vector2 value2, out float result)
        {
            float v1 = value1.x - value2.x, v2 = value1.y - value2.y;
            
            result = (v1 * v1) + (v2 * v2);
        }
        
        public override bool Equals(object obj)
        {
            if (obj is Vector2 other)
            {
                return Equals(other);
            }

            return false;
        }

        public bool Equals(Vector2 other)
        {
            return (x == other.x) && (y == other.y);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                return (x.GetHashCode() * 397) ^ y.GetHashCode();
            }
        }
        
        public float Length()
        {
            return (float)Math.Sqrt((x * x) + (y * y));
        }
        
        public float LengthSquared()
        {
            return (x * x) + (y * y);
        }
        
        public static Vector2 Lerp(Vector2 value1, Vector2 value2, float amount)
        {
            return new Vector2(
                value1.x + (value2.x - value1.x) * amount,
                value1.y + (value2.y - value1.y) * amount
            );
        }

        public static void Lerp(ref Vector2 value1, ref Vector2 value2, float amount, out Vector2 result)
        {
            result.x = value1.x + (value2.x - value1.x) * amount;
            result.y = value1.y + (value2.y - value1.y) * amount;
        }

        public void Normalize()
        {
            var val = 1.0f / (float)Math.Sqrt((x * x) + (y * y));
            
            x *= val;
            y *= val;
        }
        
        public static Vector2 Normalize(Vector2 value)
        {
            var val = 1.0f / (float)Math.Sqrt((value.x * value.x) + (value.y * value.y));
            
            value.x *= val;
            value.y *= val;
            
            return value;
        }
        
        public static void Normalize(ref Vector2 value, out Vector2 result)
        {
            var val = 1.0f / (float)Math.Sqrt((value.x * value.x) + (value.y * value.y));
            
            result.x = value.x * val;
            result.y = value.y * val;
        }
        
        public override string ToString()
        {
            return "{X:" + x + " Y:" + y + "}";
        }

        #endregion
    }
}