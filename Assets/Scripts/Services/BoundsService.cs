using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AsteroidsClone
{
    public sealed class BoundsService
    {
        #region Fields

        private readonly Camera _camera;

        #endregion

        #region Constructor

        public BoundsService()
        {
            _camera = Camera.main;

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
            var z = _camera.gameObject.transform.position.z;
            var topRight = _camera.ViewportToWorldPoint(new Vector3(1, 1, -z));
            var bottomLeft = _camera.ViewportToWorldPoint(new Vector3(0, 0, -z));

            TopBound = topRight.y;
            BottomBound = bottomLeft.y;
            LeftBound = bottomLeft.x;
            RightBound = topRight.x;
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
            var bound = (BoundSide) Random.Range(0, Enum.GetValues(typeof(BoundSide)).Length);

            switch (bound)
            {
                case BoundSide.Top:
                    position.x = Random.Range(LeftBound, RightBound);
                    position.y = TopBound + offset;
                    break;
                case BoundSide.Bottom:
                    position.x = Random.Range(LeftBound, RightBound);
                    position.y = BottomBound - offset;
                    break;
                case BoundSide.Left:
                    position.x = LeftBound - offset;
                    position.y = Random.Range(BottomBound, TopBound);
                    break;
                case BoundSide.Right:
                    position.x = RightBound + offset;
                    position.y = Random.Range(BottomBound, TopBound);
                    break;
            }

            return position;
        }

        #endregion
    }
}