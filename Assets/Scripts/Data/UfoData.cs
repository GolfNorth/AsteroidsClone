using UnityEngine;

namespace AsteroidsClone
{
    [CreateAssetMenu(fileName = "UfoData")]
    public sealed class UfoData : ScriptableObject
    {
        [SerializeField] private GameObject polygonalPrefab;
        [SerializeField] private GameObject spritePrefab;
        [SerializeField] private float speed;

        public GameObject PolygonalPrefab { get => polygonalPrefab; set => polygonalPrefab = value; }
        public GameObject SpritePrefab { get => spritePrefab; set => spritePrefab = value; }
        public float Speed { get => speed; set => speed = value; }
    }
}
