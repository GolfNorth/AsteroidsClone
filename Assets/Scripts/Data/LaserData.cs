using UnityEngine;

namespace AsteroidsClone
{
    [CreateAssetMenu(fileName = "LaserData")]
    public sealed class LaserData : ScriptableObject
    {
        [SerializeField] private GameObject polygonalPrefab;
        [SerializeField] private GameObject spritePrefab;
        [SerializeField] private float range;

        public GameObject PolygonalPrefab { get => polygonalPrefab; set => polygonalPrefab = value; }
        public GameObject SpritePrefab { get => spritePrefab; set => spritePrefab = value; }
        public float Range { get => range; set => range = value; }
    }
}
