using System;
using System.Numerics;

namespace AsteroidsCore
{
    public sealed class ShipModel : Model, INotifyDestroyable
    {
        #region Constructor

        public ShipModel(World world) : base(world)
        {
            _data = World.DataStorage.ShipData;
            _shape = World.PhysicsService.ClonePolygon(_data.shape);

            CalculateOffset();

            PositionChanged += deltaPosition => World.PhysicsService.TranslatePolygon(ref _shape, deltaPosition);
            AngleChanged += deltaAngle => World.PhysicsService.RotatePolygon(ref _shape, deltaAngle);
        }

        #endregion

        #region Fields

        private readonly ShipData _data;
        private float _offset;
        private PolygonShape _shape;

        #endregion

        #region Properties

        public PolygonShape Shape => _shape;

        public Action Destroyed { get; set; }

        public bool IsDestroyed { get; set; }

        #endregion

        #region Methods

        public void Revive()
        {
            IsDestroyed = false;

            Position = Vector2.Zero;
            Velocity = Vector2.Zero;
            Angle = 0;
        }

        public void Destroy()
        {
            IsDestroyed = true;

            Velocity = Vector2.Zero;

            Destroyed?.Invoke();
        }

        private void CalculateOffset()
        {
            _offset = 0;

            foreach (var point in _shape.Points)
            {
                var x = Math.Abs(point.X);
                var y = Math.Abs(point.Y);
                var max = x > y ? x : y;

                _offset = max > _offset ? max : _offset;
            }
        }

        public void Move(float translation, float rotation)
        {
            if (rotation != 0) Angle -= rotation * _data.angularSpeed * World.UpdateService.FixedDeltaTime;

            if (translation > 0)
            {
                Velocity += Direction * _data.speed * World.UpdateService.FixedDeltaTime;

                if (Velocity.Length() > _data.speed)
                    Velocity = Vector2.Normalize(Velocity) * _data.speed;
            }
            else
            {
                Velocity = Vector2.Distance(Velocity, Vector2.Zero) > _data.stopSpeed
                    ? Vector2.Lerp(Velocity, Vector2.Zero, (1 - _data.inertia) * World.UpdateService.FixedDeltaTime)
                    : Vector2.Zero;
            }

            var position = Position + Velocity * World.UpdateService.FixedDeltaTime;

            World.BoundsService.WrapPosition(position, ref position, _offset);

            Position = position;
        }

        #endregion
    }
}