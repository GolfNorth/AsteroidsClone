using AsteroidsCore;
using UnityEngine;

namespace AsteroidsView
{
    public sealed class AsteroidView : DestroyableView<AsteroidModel>
    {
        #region Fields

        private float _radius;

        #endregion

        #region Methods

        protected override void LateUpdate()
        {
            base.LateUpdate();

            if (_radius == Model.Radius) return;

            transform.localScale = Vector3.one * (Model.Radius * 2f);

            _radius = Model.Radius;
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(Model.Shape.Center.ToUnity(), Model.Shape.Radius);
        }

        #endregion
    }
}