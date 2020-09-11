﻿using UnityEngine;

namespace AsteroidsClone
{
    [CreateAssetMenu(fileName = "BulletData")]
    public sealed class BulletData : ViewData
    {
        [Header("Settings")]
        [SerializeField] private float range;
        [SerializeField] private float speed;

        public float Range => range;
        public float Speed => speed;
    }
}
