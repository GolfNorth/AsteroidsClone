using UnityEngine;

namespace AsteroidsClone
{
    public sealed class ShipView : DestroyableView<ShipModel>
    {
        #region Methods

        private void OnDrawGizmos()
        {
            var next = 0;

            for (var current = 0; current < Model.Shape.Points.Length; current++)
            {
                next = current + 1;

                if (next == Model.Shape.Points.Length) next = 0;

                Gizmos.DrawLine(Model.Shape.Points[current], Model.Shape.Points[next]);
            }
        }

        #endregion
    }
}