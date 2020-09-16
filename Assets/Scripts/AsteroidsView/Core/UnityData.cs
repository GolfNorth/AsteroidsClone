using UnityEngine;

namespace AsteroidsView
{
    public abstract class UnityData : ScriptableObject
    {
        #region Fields

        [Header("View")]
        public GameObject polygonalPrefab;

        public GameObject spritePrefab;

        #endregion
    }
}