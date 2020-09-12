using UnityEngine;

namespace AsteroidsClone
{
    public sealed class LaserView : View<LaserModel>
    {
        #region Methods

        private void OnDrawGizmos()
        {
            Gizmos.DrawLine(Model.Shape.PointA, Model.Shape.PointB);
        }

        #endregion
    }
}