using AsteroidsCore;
using UnityEngine;

namespace AsteroidsView
{
    public sealed class UfoView : DestroyableView<UfoModel>
    {
        #region Properties

        private void OnDrawGizmos()
        {
            var next = 0;

            for (var current = 0; current < Model.Shape.Points.Length; current++)
            {
                next = current + 1;

                if (next == Model.Shape.Points.Length) next = 0;

                Gizmos.DrawLine(Model.Shape.Points[current].ToUnity(), Model.Shape.Points[next].ToUnity());
            }
        }

        #endregion
    }
}