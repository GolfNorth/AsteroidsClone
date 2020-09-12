using UnityEngine;

namespace AsteroidsClone
{
    public sealed class LaserModel : Model
    {
        #region Constructor

        public LaserModel(LaserData data, World world) : base(world)
        {
            _data = data;
            _shape = new LineShape
            {
                Center = new Vector2(),
                PointA = new Vector2(),
                PointB = new Vector2(_data.Range, 0)
            };

            PositionChanged += deltaPosition => World.PhysicsService.TranslateLine(ref _shape, deltaPosition);
            AngleChanged += deltaAngle => World.PhysicsService.RotateLine(ref _shape, deltaAngle);
        }

        #endregion

        #region Properties

        public LineShape Shape => _shape;

        #endregion

        #region Methods

        public void Move()
        {
            Position = World.Ship.Position;
            Angle = World.Ship.Angle;
        }

        #endregion

        #region Fields

        private readonly LaserData _data;
        private LineShape _shape;

        #endregion
    }
}