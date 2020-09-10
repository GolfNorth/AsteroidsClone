using System.Collections.Generic;
using UnityEngine;

namespace AsteroidsClone
{
    [CreateAssetMenu(fileName = "AsteroidData")]
    public sealed class AsteroidData : ScriptableObject
    {
        [SerializeField] private GameObject polygonalPrefab;
        [SerializeField] private GameObject spritePrefab;
        [SerializeField] private AsteroidSettings[] settings;
        private Dictionary<AsteroidSize, AsteroidSettings> _settings;

        public GameObject PolygonalPrefab => polygonalPrefab;
        public GameObject SpritePrefab => spritePrefab;
        public Dictionary<AsteroidSize, AsteroidSettings> Settings => _settings;

        private void Awake()
        {
            _settings = new Dictionary<AsteroidSize, AsteroidSettings>();

            foreach (var s in settings)
            {
                if (_settings.ContainsKey(s.AsteroidSize)) continue;

                _settings.Add(s.AsteroidSize, s);
            }
        }
    }
}
