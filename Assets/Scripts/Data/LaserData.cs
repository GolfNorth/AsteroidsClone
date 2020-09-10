using UnityEngine;

namespace AsteroidsClone
{
    [CreateAssetMenu(fileName = "LaserData")]
    public sealed class LaserData : ScriptableObject
    {
        [SerializeField] private GameObject polygonalPrefab;
        [SerializeField] private GameObject spritePrefab;
        [SerializeField] private float range;

        public GameObject PolygonalPrefab => polygonalPrefab;
        public GameObject SpritePrefab => spritePrefab;
        public float Range => range;
    }
}
