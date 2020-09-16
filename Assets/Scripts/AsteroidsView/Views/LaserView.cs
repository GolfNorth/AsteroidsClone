using AsteroidsCore;
using UnityEngine;

namespace AsteroidsView
{
    public sealed class LaserView : BaseView<LaserModel>
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
            _lineRenderer.SetPosition(0, Model.Shape.PointA.ToUnity());
            _lineRenderer.SetPosition(1, Model.Shape.PointB.ToUnity());
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawLine(Model.Shape.PointA.ToUnity(), Model.Shape.PointB.ToUnity());
        }

        #endregion
    }
}