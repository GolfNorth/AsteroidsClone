using UnityEngine;

namespace AsteroidsClone
{
    [CreateAssetMenu(fileName = "LaserData")]
    public sealed class LaserData : Data
    {
        #region Fields

        [Header("Settings")]
        [SerializeField] private float range;
        [SerializeField] private float duration;
        [SerializeField] private float cooldown;

        #endregion

        #region Properties

        public float Range => range;
        public float Duration => duration;
        public float Cooldown => cooldown;

        #endregion
    }
}