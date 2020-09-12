using UnityEngine;

namespace AsteroidsClone
{
    public sealed class BulletModel : Model
    {
        private const float Offset = 5f;
        private BulletData _data;

        public bool IsInsideField
        {
            get
            {
                if (Position.x < World.BoundsService.LeftBound - Offset) return false;

                if (Position.x > World.BoundsService.RightBound + Offset) return false;

                if (Position.y < World.BoundsService.BottomBound - Offset) return false;

                if (Position.y > World.BoundsService.TopBound + Offset) return false;

                return true;
            }
        }

        public BulletModel(BulletData data, World world) : base(world)
        {
            _data = data;

            AngleChanged += (_) => Velocity = Direction * _data.Speed;
        }

        public void Move()
        {
            Position += Velocity * Time.fixedDeltaTime;
        }
    }
}
