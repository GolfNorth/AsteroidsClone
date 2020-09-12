using System.Collections;
using UnityEngine;

namespace AsteroidsClone
{
    public sealed class UfoView : View<UfoModel>
    {
        [SerializeField] private float destroyDelay = 1f;

        public bool IsDestroyed { get; private set; }

        protected override void OnEnable()
        {
            base.OnEnable();

            IsDestroyed = false;
        }

        protected override void OnModelChanged()
        {
            Model.Destroyed += () => { StartCoroutine(Destroy()); };
        }

        private IEnumerator Destroy()
        {
            yield return new WaitForSeconds(destroyDelay);

            IsDestroyed = true;
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
    }
}
