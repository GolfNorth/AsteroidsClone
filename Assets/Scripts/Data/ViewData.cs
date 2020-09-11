﻿using UnityEngine;

namespace AsteroidsClone
{
    public abstract class ViewData : ScriptableObject
    {
        [Header("View")]
        [SerializeField] private GameObject polygonalPrefab;
        [SerializeField] private GameObject spritePrefab;

        public GameObject PolygonalPrefab => polygonalPrefab;
        public GameObject SpritePrefab => spritePrefab;
    }
}
