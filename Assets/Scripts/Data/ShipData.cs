using UnityEngine;

namespace AsteroidsClone
{
    [CreateAssetMenu(fileName = "ShipData")]
    public sealed class ShipData : ScriptableObject
    {
        [SerializeField] private GameObject polygonalPrefab;
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
