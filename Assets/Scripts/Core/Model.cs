using System;
using UnityEngine;

namespace AsteroidsClone
{
    public abstract class Model : Resident, IActivatable
    {
        private bool _isActive;
        private ViewMode _viewMode;
        private Vector2 _position;
        private Vector2 _velocity;
        private float _angle;

        public Action IsActiveChanged;
        public Action ViewModeChanged;
        public Action PositionChanged;
        public Action VelocityChanged;
        public Action AngleChanged;
        public Action SpeedChanged;

        public Model(World world) : base(world)
        {
        }

        public bool IsActive
        {
            get => _isActive;
            set {
                if (_isActive == value) return;

                _isActive = value;

                IsActiveChanged?.Invoke();
            }
        }

        public ViewMode ViewMode
        {
            get => _viewMode;
            set
            {
                if (_viewMode == value) return;

                _viewMode = value;

                ViewModeChanged?.Invoke();
            }
        }

        public Vector2 Position
        {
            get => _position;
            set
            {
                if (_position == value) return;

                _position = value;

                PositionChanged?.Invoke();
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

                _angle = value;

                AngleChanged?.Invoke();
            }
        }

        public Vector2 Direction
        {
            get
            {
                var direction = new Vector2
                {
                    x = Mathf.Cos(Angle * Mathf.Deg2Rad),
                    y = Mathf.Sin(Angle * Mathf.Deg2Rad)
                };

                return direction.normalized;
            }
        }
    }
}
