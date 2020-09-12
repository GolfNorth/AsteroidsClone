using UnityEngine;

namespace AsteroidsClone
{
    public abstract class Data : ScriptableObject
    {
        #region Fields

        [Header("View")] [SerializeField] private GameObject polygonalPrefab;

        [SerializeField] private GameObject spritePrefab;

        #endregion

        #region Properties

        public GameObject PolygonalPrefab => polygonalPrefab;
        public GameObject SpritePrefab => spritePrefab;

        #endregion
    }
}