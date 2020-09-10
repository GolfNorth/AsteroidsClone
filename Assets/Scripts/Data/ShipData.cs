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

        public GameObject PolygonalPrefab => polygonalPrefab;
        public GameObject SpritePrefab => spritePrefab;
        public float Acceleration => acceleration;
        public float AngularSpeed => angularSpeed;
        public float MaximumSpeed => maximumSpeed;
    }
}
