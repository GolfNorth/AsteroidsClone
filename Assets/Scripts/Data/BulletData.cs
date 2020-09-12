using UnityEngine;

namespace AsteroidsClone
{
    [CreateAssetMenu(fileName = "BulletData")]
    public sealed class BulletData : Data
    {
        #region Fields

        [Header("Settings")]
        [SerializeField] private float speed;
        [SerializeField] private float rapidity;

        #endregion

        #region Properties

        public float Speed => speed;
        public float Rapidity => rapidity;

        #endregion
    }
}