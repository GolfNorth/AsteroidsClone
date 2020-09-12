using UnityEngine;

namespace AsteroidsClone
{
    public sealed class LaserView : View<LaserModel>
    {
        #region Fields

        private LineRenderer _lineRenderer;

        #endregion
        
        #region Methods

        private void Awake()
        {
            _lineRenderer = GetComponent<LineRenderer>();
            _lineRenderer.positionCount = 2;
            _lineRenderer.loop = false;
        }

        private void LateUpdate()
        {
            _lineRenderer.SetPosition(0, Model.Shape.PointA);
            _lineRenderer.SetPosition(1, Model.Shape.PointB);
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawLine(Model.Shape.PointA, Model.Shape.PointB);
        }

        #endregion
    }
}