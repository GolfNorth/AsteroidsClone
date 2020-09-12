using UnityEngine;

namespace AsteroidsClone
{
    public sealed class BulletView : View<BulletModel>
    {
        #region Methods

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(Model.Position, 0.01f);
        }

        #endregion
    }
}