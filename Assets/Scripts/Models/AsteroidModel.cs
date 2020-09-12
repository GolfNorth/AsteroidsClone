using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AsteroidsClone
{
    public sealed class AsteroidModel : Model, INotifyDestroyable
    {
        #region Constructor

        public AsteroidModel(AsteroidData data, World world) : base(world)
        {
            _data = data;
            _shape = new CircleShape();

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
                Radius = _size != AsteroidSize.None ? _data.Settings[value].Radius : 0;
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

        public void RandomizeAngleAndSpeed()
        {
            Angle = Random.Range(0, 360);
            Speed = Random.Range(_data.Settings[Size].MinSpeed, _data.Settings[Size].MaxSpeed);

            Velocity = Direction * Speed;
        }

        public void RandomizePosition()
        {
            Position = World.BoundsService.RandomizePosition(Radius);
        }

        public void RandomizeSize()
        {
            Size = (AsteroidSize) Random.Range(1, Enum.GetValues(typeof(AsteroidSize)).Length - 1);
        }

        public void Move()
        {
            var position = Position + Velocity * Time.fixedDeltaTime;

            World.BoundsService.WrapPosition(position, ref position, Radius);

            Position = position;
        }

        #endregion
    }
}