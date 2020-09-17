using System;
using System.Numerics;

namespace AsteroidsCore
{
    public sealed class BoundsService : Resident
    {
        #region Constructor

        public BoundsService(World world) : base(world)
        {
            RecalculateBounds();
        }

        #endregion

        #region Properties

        public float TopBound { get; private set; }

        public float BottomBound { get; private set; }

        public float LeftBound { get; private set; }

        public float RightBound { get; private set; }

        #endregion

        #region Methods

        private void RecalculateBounds()
        {
            TopBound = World.OffsetY;
            BottomBound = -World.Height + World.OffsetY;
            LeftBound = World.OffsetX;
            RightBound = World.Width + World.OffsetX;
        }

        public void WrapPosition(Vector2 input, ref Vector2 output, float offset = 0)
        {
            output = input;

            if (input.X < LeftBound - offset)
                output.X = RightBound + offset;

            if (input.X >= RightBound + offset)
                output.X = LeftBound - offset;

            if (input.Y < BottomBound - offset)
                output.Y = TopBound + offset;

            if (input.Y >= TopBound + offset)
                output.Y = BottomBound - offset;
        }

        public Vector2 RandomizePosition(float offset = 0)
        {
            var position = new Vector2();
            var bound = (BoundSide) World.Random.Next(0, Enum.GetValues(typeof(BoundSide)).Length);

            switch (bound)
            {
                case BoundSide.Top:
                    position.X = LeftBound + (float) World.Random.NextDouble() * World.Width;
                    position.Y = TopBound + offset;
                    break;
                case BoundSide.Bottom:
                    position.X = LeftBound + (float) World.Random.NextDouble() * World.Width;
                    position.Y = BottomBound - offset;
                    break;
                case BoundSide.Left:
                    position.X = LeftBound - offset;
                    position.Y = BottomBound + (float) World.Random.NextDouble() * World.Height;
                    break;
                case BoundSide.Right:
                    position.X = RightBound + offset;
                    position.Y = BottomBound + (float) World.Random.NextDouble() * World.Height;
                    break;
            }

            return position;
        }

        #endregion
    }
}