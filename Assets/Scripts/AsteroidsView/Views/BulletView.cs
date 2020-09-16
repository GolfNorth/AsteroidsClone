using AsteroidsCore;
using UnityEngine;

namespace AsteroidsView
{
    public sealed class BulletView : BaseView<BulletModel>
    {
        #region Methods

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(Model.Position.ToUnity(), 0.01f);
        }

        #endregion
    }
}