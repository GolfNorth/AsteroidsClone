using System.Collections;
using UnityEngine;

namespace AsteroidsClone
{
    public sealed class ShipView : View<ShipModel>
    {
        #region Fields

        [SerializeField] private float destroyDelay = 1f;

        #endregion

        #region Properties

        public bool IsDestroyed { get; private set; }

        #endregion

        #region Methods

        protected override void OnEnable()
        {
            base.OnEnable();

            IsDestroyed = Model?.IsDestroyed ?? false;
        }

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

        protected override void OnModelChanged()
        {
            Model.Destroyed += () =>
            {
                if (gameObject.activeSelf)
                    StartCoroutine(Destroy());
                else
                    IsDestroyed = true;
            };
        }

        private IEnumerator Destroy()
        {
            yield return new WaitForSeconds(destroyDelay);

            IsDestroyed = true;
        }

        #endregion
    }
}