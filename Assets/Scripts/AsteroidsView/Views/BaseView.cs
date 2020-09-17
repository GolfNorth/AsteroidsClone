using AsteroidsCore;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

namespace AsteroidsView
{
    public abstract class BaseView<TModel> : MonoBehaviour, IView<TModel> where TModel : Model
    {
        #region Fields

        private TModel _model;

        #endregion

        #region Properties

        public ViewMode ViewMode { get; set; }
        public Context Context { get; set; }

        public bool IsActive
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }

        public bool IsDestroyed { get; protected set; }
        public Vector2 Position { get; set; }
        public float Rotation { get; set; }

        public TModel Model
        {
            protected get => _model;
            set
            {
                _model = value;

                _model.IsActiveChanged += () => IsActive = _model.IsActive && Context.ViewMode == ViewMode;
                _model.PositionChanged += _ => transform.position = _model.Position.ToUnity();
                _model.AngleChanged += _ => transform.rotation = Quaternion.Euler(0, 0, _model.Angle);

                SyncView();
                OnModelChanged();
            }
        }

        #endregion

        #region Methods

        protected virtual void OnEnable()
        {
            if (Model is null) return;

            SyncView();
        }

        private void SyncView()
        {
            IsActive = _model.IsActive && Context.ViewMode == ViewMode;
            transform.position = _model.Position.ToUnity();
            transform.rotation = Quaternion.Euler(0, 0, _model.Angle);
        }

        protected virtual void OnModelChanged()
        {
        }

        public virtual void Revive()
        {
        }

        public virtual void Destroy()
        {
        }

        #endregion
    }
}