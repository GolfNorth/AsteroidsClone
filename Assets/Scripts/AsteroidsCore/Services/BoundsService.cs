using System;

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
            BottomBound = - World.Height + World.OffsetY;
            LeftBound = World.OffsetX;
            RightBound = World.Width + World.OffsetX;
        }

        public void WrapPosition(Vector2 input, ref Vector2 output, float offset = 0)
        {
            output = input;

            if (input.x < LeftBound - offset)
                output.x = RightBound + offset;

            if (input.x >= RightBound + offset)
                output.x = LeftBound - offset;

            if (input.y < BottomBound - offset)
                output.y = TopBound + offset;

            if (input.y >= TopBound + offset)
                output.y = BottomBound - offset;
        }

        public Vector2 RandomizePosition(float offset = 0)
        {
            var position = new Vector2();
            var bound = (BoundSide) World.Random.Next(0, Enum.GetValues(typeof(BoundSide)).Length);

            switch (bound)
            {
                case BoundSide.Top:
                    position.x = LeftBound + (float)World.Random.NextDouble() * World.Width;
                    position.y = TopBound + offset;
                    break;
                case BoundSide.Bottom:
                    position.x =  LeftBound + (float)World.Random.NextDouble() * World.Width;
                    position.y = BottomBound - offset;
                    break;
                case BoundSide.Left:
                    position.x = LeftBound - offset;
                    position.y = BottomBound + (float)World.Random.NextDouble() * World.Height;
                    break;
                case BoundSide.Right:
                    position.x = RightBound + offset;
                    position.y = BottomBound + (float)World.Random.NextDouble() * World.Height;
                    break;
            }

            return position;
        }

        #endregion
    }
}