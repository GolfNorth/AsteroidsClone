using System.Collections;
using AsteroidsCore;
using UnityEngine;

namespace AsteroidsView
{
    public abstract class DestroyableView<TModel> : BaseView<TModel> where TModel : Model, INotifyDestroyable
    {
        #region Fields

        [SerializeField] private float destroyDelay = 1f;
        private ParticleSystem _particleSystem;
        private SpriteRenderer _spriteRenderer;
        private MeshRenderer _meshRenderer;

        #endregion

        #region Methods

        protected virtual void Awake()
        {
            _particleSystem = GetComponent<ParticleSystem>();
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            _meshRenderer = GetComponentInChildren<MeshRenderer>();
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            IsDestroyed = Model?.IsDestroyed ?? false;
        }

        protected override void OnModelChanged()
        {
            Model.Destroyed += () =>
            {
                if (gameObject.activeSelf)
                    StartCoroutine(DestroyCoroutine());
                else
                    IsDestroyed = true;
            };
        }

        protected virtual void LateUpdate()
        {
            if (Model.IsDestroyed) return;
            
            if (_spriteRenderer != null && !_spriteRenderer.enabled) 
                _spriteRenderer.enabled = true;
            
            if (_meshRenderer != null && !_meshRenderer.enabled) 
                _meshRenderer.enabled = true;
        }

        private IEnumerator DestroyCoroutine()
        {
            IsDestroyed = false;
            
            DestroyBegin();
            
            if (_particleSystem != null) _particleSystem.Play();
            
            if (_spriteRenderer != null) _spriteRenderer.enabled = false;
            
            if (_meshRenderer != null) _meshRenderer.enabled = false;
            
            yield return new WaitForSeconds(destroyDelay);

            IsDestroyed = true;
        }

        protected virtual void DestroyBegin()
        {
            
        }

        #endregion
    }
}