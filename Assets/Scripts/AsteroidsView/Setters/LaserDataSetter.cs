using AsteroidsCore;
using UnityEngine;

namespace AsteroidsView
{
    [CreateAssetMenu(fileName = "LaserData")]
    public sealed class LaserDataSetter : UnityData
    {
        #region Fields

        [Header("Settings")]
        public LaserData data;

        #endregion
    }
}