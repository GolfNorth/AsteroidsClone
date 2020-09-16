using AsteroidsCore;
using UnityEngine;

namespace AsteroidsView
{
    [CreateAssetMenu(fileName = "BulletData")]
    public sealed class BulletDataSetter : UnityData
    {
        #region Fields

        [Header("Settings")]
        public BulletData data;

        #endregion
    }
}