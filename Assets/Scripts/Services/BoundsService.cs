using UnityEngine;

namespace AsteroidsClone
{
    public sealed class BoundsService
    {
        private Camera _camera;
        private float _topBound;
        private float _bottomBound;
        private float _leftBound;
        private float _rightBound;

        public float TopBound => _topBound;

        public float BottomBound => _bottomBound;

        public float LeftBound => _leftBound;

        public float RightBound => _rightBound;

        public BoundsService()
        {
            _camera = Camera.main;

            RecalculateBounds();
        }

        private void RecalculateBounds()
        {
            var z = _camera.gameObject.transform.position.z;
            var topRight = _camera.ViewportToWorldPoint(new Vector3(1, 1, -z));
            var bottomLeft = _camera.ViewportToWorldPoint(new Vector3(0, 0, -z));

            _topBound = topRight.y;
            _bottomBound = bottomLeft.y;
            _leftBound = bottomLeft.x;
            _rightBound = topRight.x;
        }

        public void WrapCoordinates(Vector2 input, ref Vector2 output, float offset = 0)
        {
            output = input;

            if (input.x < _leftBound - offset)
                output.x = _rightBound + offset;

            if (input.x >= _rightBound + offset)
                output.x = _leftBound - offset;

            if (input.y < _bottomBound - offset)
                output.y = _topBound + offset;

            if (input.y >= _topBound + offset)
                output.y = _bottomBound - offset;
        }

        public Vector2 RandomizePosition(float offset = 0)
        {
            var position = new Vector2();
            var bound = (BoundSide)Random.Range(0, System.Enum.GetValues(typeof(BoundSide)).Length);

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
    }
}