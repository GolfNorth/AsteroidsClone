using System;
using UnityEngine;

namespace AsteroidsClone
{
    public sealed class UfoModel : Model
    {
        private UfoData _data;
        private PolygonShape _shape;
        private float _offset;

        public Action Destroyed;

        public PolygonShape Shape => _shape;

        public float Speed { get; set; }

        public bool IsDestroyed { get; set; }

        public UfoModel(UfoData data, World world) : base(world)
        {
            _data = data;
            _shape = World.PhysicsService.ClonePolygon(_data.Shape);

            CalculateOffset();

            PositionChanged += (deltaPosition) => World.PhysicsService.TranslatePolygon(ref _shape, deltaPosition);
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
    }
}
