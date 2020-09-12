using System;
using UnityEngine;

namespace AsteroidsClone
{
    public sealed class ShipModel : Model, IDestroyable
    {
        #region Constructor

        public ShipModel(ShipData data, World world) : base(world)
        {
            _data = data;
            _shape = World.PhysicsService.ClonePolygon(_data.Shape);

            CalculateOffset();

            PositionChanged += deltaPosition => World.PhysicsService.TranslatePolygon(ref _shape, deltaPosition);
            AngleChanged += deltaAngle => World.PhysicsService.RotatePolygon(ref _shape, deltaAngle);
        }

        #endregion

        #region Fields

        private readonly ShipData _data;
        private float _offset;
        private PolygonShape _shape;

        public Action Destroyed;

        #endregion

        #region Properties

        public PolygonShape Shape => _shape;

        public bool IsDestroyed { get; set; }

        #endregion

        #region Methods

        public void Revive()
        {
            IsDestroyed = false;

            Position = Vector2.zero;
            Velocity = Vector2.zero;
            Angle = 0;
        }

        public void Destroy()
        {
            IsDestroyed = true;

            Velocity = Vector2.zero;

            Destroyed?.Invoke();
        }

        private void CalculateOffset()
        {
            _offset = 0;

            foreach (var point in _shape.Points)
            {
                var x = Mathf.Abs(point.x);
                var y = Mathf.Abs(point.y);
                var max = x > y ? x : y;

                _offset = max > _offset ? max : _offset;
            }
        }

        public void Move(float translation, float rotation)
        {
            if (rotation != 0) Angle -= rotation * _data.AngularSpeed * Time.fixedDeltaTime;

            if (translation > 0)
            {
                Velocity += Direction * _data.Speed * Time.fixedDeltaTime;

                if (Velocity.magnitude > _data.Speed)
                    Velocity = Velocity.normalized * _data.Speed;
            }
            else
            {
                Velocity = Vector2.Distance(Velocity, Vector2.zero) > _data.StopSpeed
                    ? Vector2.Lerp(Velocity, Vector2.zero, (1 - _data.Inertia) * Time.fixedDeltaTime)
                    : Vector2.zero;
            }

            var position = Position + Velocity * Time.fixedDeltaTime;

            World.BoundsService.WrapPosition(position, ref position, _offset);

            Position = position;
        }

        #endregion
    }
}