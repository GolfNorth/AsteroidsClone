using UnityEngine;

namespace AsteroidsClone
{
    [CreateAssetMenu(fileName = "UfoData")]
    public sealed class UfoData : Data
    {
        #region Fields

        [Header("Settings")]
        [SerializeField] private float spawnDelay;
        [SerializeField] private PolygonShape shape;
        [SerializeField] private float speed;

        #endregion

        #region Properties

        public float SpawnDelay => spawnDelay;
        public PolygonShape Shape => shape;
        public float Speed => speed;

        #endregion
    }
}