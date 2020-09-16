using AsteroidsCore;
using UnityEngine;

namespace AsteroidsView
{
    [CreateAssetMenu(fileName = "UfoData")]
    public sealed class UfoDataSetter : UnityData
    {
        #region Fields

        [Header("Settings")]
        public UfoData data;

        #endregion
    }
}