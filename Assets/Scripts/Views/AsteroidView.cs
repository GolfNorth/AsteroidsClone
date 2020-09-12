using UnityEngine;

namespace AsteroidsClone
{
    public sealed class AsteroidView : DestroyableView<AsteroidModel>
    {
        #region Methods

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(Model.Shape.Center, Model.Shape.Radius);
        }

        #endregion
    }
}