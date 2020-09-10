using UnityEngine;

namespace AsteroidsClone
{
    [CreateAssetMenu(fileName = "AsteroidData")]
    public sealed class AsteroidData : ScriptableObject
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private GameObject spritePrefab;
        [SerializeField] private float acceleration;
        [SerializeField] private float angularSpeed;
        [SerializeField] private float maximumSpeed;

        public GameObject PolygonalPrefab { get => polygonalPrefab; set => polygonalPrefab = value; }
        public GameObject SpritePrefab { get => spritePrefab; set => spritePrefab = value; }
        public float Acceleration { get => acceleration; set => acceleration = value; }
        public float AngularSpeed { get => angularSpeed; set => angularSpeed = value; }
        public float MaximumSpeed { get => maximumSpeed; set => maximumSpeed = value; }
    }
}
