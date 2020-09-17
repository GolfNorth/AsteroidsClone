using System;
using System.Numerics;

namespace AsteroidsCore
{
    public abstract class Model : Resident, IModel, IActivatable
    {
        #region Constructor

        protected Model(World world) : base(world)
        {
        }

        #endregion

        #region Fields

        private float _angle;
        private bool _isActive;
        private Vector2 _position;
        private Vector2 _velocity;
        public Action<float> AngleChanged;

        public Action IsActiveChanged;
        public Action<Vector2> PositionChanged;
        public Action VelocityChanged;

        #endregion

        #region Properties

        public Vector2 Position
        {
            get => _position;
            set
            {
                if (_position == value) return;

                var deltaPosition = value - _position;

                _position = value;

                PositionChanged?.Invoke(deltaPosition);
            }
        }

        public Vector2 Velocity
        {
            get => _velocity;
            set
            {
                if (_velocity == value) return;

                _velocity = value;

                VelocityChanged?.Invoke();
            }
        }

        public float Angle
        {
            get => _angle;
            set
            {
                if (_angle == value) return;

                var deltaAngle = value - _angle;

                _angle = value;

                AngleChanged?.Invoke(deltaAngle);
            }
        }

        public Vector2 Direction
        {
            get
            {
                var angle = (float) Math.PI * Angle / 180f;
                var direction = new Vector2
                {
                    X = (float) Math.Cos(angle),
                    Y = (float) Math.Sin(angle)
                };

                return Vector2.Normalize(direction);
            }
        }

        public bool IsActive
        {
            get => _isActive;
            set
            {
                if (_isActive == value) return;

                _isActive = value;

                IsActiveChanged?.Invoke();
            }
        }

        #endregion
    }
}