using System;
using System.Numerics;

namespace AsteroidsCore
{
    public sealed class UfoModel : Model, INotifyDestroyable
    {
        #region Constructor

        public UfoModel(World world) : base(world)
        {
            _data = World.DataStorage.UfoData;
            _shape = World.PhysicsService.ClonePolygon(_data.shape);

            CalculateOffset();

            PositionChanged += deltaPosition => World.PhysicsService.TranslatePolygon(ref _shape, deltaPosition);
        }

        #endregion

        #region Fields

        private readonly UfoData _data;
        private float _offset;
        private PolygonShape _shape;

        #endregion

        #region Properties

        public PolygonShape Shape => _shape;

        public Action Destroyed { get; set; }

        public float Speed { get; set; }

        public bool IsDestroyed { get; set; }

        #endregion

        #region Methods

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

        public void RandomizePosition()
        {
            Position = World.BoundsService.RandomizePosition(_offset);
        }

        public void Move()
        {
            Velocity = Vector2.Normalize(World.Ship.Position - Position) * _data.speed;

            var position = Position + Velocity * World.UpdateService.FixedDeltaTime;

            World.BoundsService.WrapPosition(position, ref position, _offset);

            Position = position;
        }

        #endregion
    }
}