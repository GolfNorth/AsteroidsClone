using AsteroidsCore;
using UnityEngine;

namespace AsteroidsView
{
    [CreateAssetMenu(fileName = "ShipData")]
    public sealed class ShipDataSetter : UnityData
    {
        #region MyRegion

        [Header("Settings")]
        public ShipData data;

        #endregion;
    }
}