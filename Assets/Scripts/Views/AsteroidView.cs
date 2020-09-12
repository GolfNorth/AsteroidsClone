using System.Collections;
using UnityEngine;

namespace AsteroidsClone
{
    public sealed class AsteroidView : View<AsteroidModel>
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
            Gizmos.DrawWireSphere(Model.Shape.Center, Model.Shape.Radius);
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