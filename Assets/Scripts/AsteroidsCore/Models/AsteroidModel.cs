using System;
using System.Numerics;

namespace AsteroidsCore
{
    public sealed class AsteroidModel : Model, INotifyDestroyable
    {
        #region Constructor

        public AsteroidModel(World world) : base(world)
        {
            _shape = new CircleShape();
            _data = World.DataStorage.AsteroidData;

            PositionChanged += deltaPosition => World.PhysicsService.TranslateCircle(ref _shape, deltaPosition);
        }

        #endregion

        #region Fields

        private readonly AsteroidData _data;
        private CircleShape _shape;
        private AsteroidSize _size;

        #endregion

        #region Properties

        public AsteroidSize Size
        {
            get => _size;
            set
            {
                if (_size == value) return;

                _size = value;
                Radius = _size != AsteroidSize.None ? _data.UnitData[value].radius : 0;
                _shape.Radius = Radius;
            }
        }

        public CircleShape Shape => _shape;

        public Action Destroyed { get; set; }

        public float Radius { get; set; }

        public float Speed { get; set; }

        public bool IsDestroyed { get; set; }

        #endregion

        #region Methods

        public void Revive()
        {
            IsDestroyed = false;

            Size = AsteroidSize.None;
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

        public void RandomizeAngleAndSpeed()
        {
            Angle = (float) World.Random.NextDouble() * 360f;
            Speed = _data.UnitData[Size].minSpeed + (float) World.Random.NextDouble()
                * (_data.UnitData[Size].maxSpeed - _data.UnitData[Size].minSpeed);

            Velocity = Direction * Speed;
        }

        public void RandomizePosition()
        {
            Position = World.BoundsService.RandomizePosition(Radius);
        }

        public void RandomizeSize()
        {
            Size = (AsteroidSize) World.Random.Next(1, Enum.GetValues(typeof(AsteroidSize)).Length - 1);
        }

        public void Move()
        {
            var position = Position + Velocity * World.UpdateService.FixedDeltaTime;

            World.BoundsService.WrapPosition(position, ref position, Radius);

            Position = position;
        }

        #endregion
    }
}