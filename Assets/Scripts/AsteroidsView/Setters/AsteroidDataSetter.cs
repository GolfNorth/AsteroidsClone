using System.Collections.Generic;
using AsteroidsCore;
using UnityEngine;

namespace AsteroidsView
{
    [CreateAssetMenu(fileName = "AsteroidData")]
    public sealed class AsteroidDataSetter : UnityData
    {
        #region Fields

        [Header("Settings")]
        public AsteroidData data;

        [SerializeField] private AsteroidData.AsteroidUnitData[] asteroidSize;

        private void OnEnable()
        {
            data.UnitData = new Dictionary<AsteroidSize, AsteroidData.AsteroidUnitData>();
            
            foreach (var s in asteroidSize)
            {
                if (data.UnitData.ContainsKey(s.asteroidSize)) continue;

                data.UnitData.Add(s.asteroidSize, s);
            }
        }

        #endregion
    }
}