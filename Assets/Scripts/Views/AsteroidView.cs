using System.Collections;
using UnityEngine;

namespace AsteroidsClone
{
    public sealed class AsteroidView : View<AsteroidModel>
    {
        [SerializeField] private float destroyDelay = 1f;

        public bool IsDestroyed { get; private set; }

        protected override void OnEnable()
        {
            base.OnEnable();

            IsDestroyed = Model is null ? false : Model.IsDestroyed;
        }

        protected override void OnModelChanged()
        {
            Model.Destroyed += () => {
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

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(Model.Shape.Center, Model.Shape.Radius);
        }
    }
}
