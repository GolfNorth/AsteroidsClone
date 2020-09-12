using System;
using UnityEngine;

namespace AsteroidsClone
{
    public sealed class UfoModel : Model
    {
        #region Constructor

        public UfoModel(UfoData data, World world) : base(world)
        {
            _data = data;
            _shape = World.PhysicsService.ClonePolygon(_data.Shape);

            CalculateOffset();

            PositionChanged += deltaPosition => World.PhysicsService.TranslatePolygon(ref _shape, deltaPosition);
        }

        #endregion

        #region Fields

        private readonly UfoData _data;
        private float _offset;
        private PolygonShape _shape;

        public Action Destroyed;

        #endregion

        #region Properties

        public PolygonShape Shape => _shape;

        public float Speed { get; set; }

        public bool IsDestroyed { get; set; }

        #endregion

        #region Methods

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

        public void RandomizePosition()
        {
            Position = World.BoundsService.RandomizePosition(_offset);
        }

        public void Move()
        {
            Velocity = (World.Ship.Position - Position).normalized * _data.Speed;

            var position = Position + Velocity * Time.fixedDeltaTime;

            World.BoundsService.WrapPosition(position, ref position, _offset);

            Position = position;
        }

        #endregion
    }
}