using UnityEngine;

namespace AsteroidsClone
{
    public sealed class BulletModel : Model
    {
        #region Constructor

        public BulletModel(BulletData data, World world) : base(world)
        {
            _data = data;

            Velocity = Direction * _data.Speed;

            AngleChanged += _ => Velocity = Direction * _data.Speed;
        }

        #endregion

        #region Properties

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

        #endregion

        #region Methods

        public void Move()
        {
            Position += Velocity * Time.fixedDeltaTime;
        }

        #endregion

        #region Fields

        private const float Offset = 5f;
        private readonly BulletData _data;

        #endregion
    }
}