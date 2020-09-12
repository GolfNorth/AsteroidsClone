using System.Collections.Generic;
using UnityEngine;

namespace AsteroidsClone
{
    [CreateAssetMenu(fileName = "AsteroidData")]
    public sealed class AsteroidData : Data
    {
        #region Fields

        [Header("Settings")]
        [SerializeField] private float spawnDelay;
        [SerializeField] private AsteroidSettings[] settings;
        private Dictionary<AsteroidSize, AsteroidSettings> _settings;

        #endregion

        #region Properties

        public float SpawnDelay => spawnDelay;

        public Dictionary<AsteroidSize, AsteroidSettings> Settings
        {
            get
            {
                if (_settings != null) return _settings;

                _settings = new Dictionary<AsteroidSize, AsteroidSettings>();

                foreach (var s in settings)
                {
                    if (_settings.ContainsKey(s.AsteroidSize)) continue;

                    _settings.Add(s.AsteroidSize, s);
                }

                return _settings;
            }
        }

        #endregion
    }
}