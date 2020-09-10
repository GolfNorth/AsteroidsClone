using UnityEngine;

namespace AsteroidsClone
{
    [CreateAssetMenu(fileName = "BulletData")]
    public sealed class BulletData : ScriptableObject
    {
        [SerializeField] private GameObject polygonalPrefab;
        [SerializeField] private GameObject spritePrefab;
        [SerializeField] private float range;
        [SerializeField] private float speed;

        public GameObject PolygonalPrefab { get => polygonalPrefab; set => polygonalPrefab = value; }
        public GameObject SpritePrefab { get => spritePrefab; set => spritePrefab = value; }
        public float Range { get => range; set => range = value; }
        public float Speed { get => speed; set => speed = value; }
    }
}
