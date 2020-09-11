﻿using UnityEngine;

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

        public void WrapCoordinates(Vector2 input, ref Vector2 output)
        {
            output = input;

            if (input.x < _leftBound)
                output.x = _rightBound;

            if (input.x >= _rightBound)
                output.x = _leftBound;

            if (input.y < _bottomBound)
                output.y = _topBound;

            if (input.y >= _topBound)
                output.y = _bottomBound;
        }
    }
}