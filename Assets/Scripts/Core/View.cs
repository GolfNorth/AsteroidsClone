using UnityEngine;

namespace AsteroidsClone
{
    public abstract class View<TModel> : MonoBehaviour where TModel : Model
    {
        private TModel _model;

        public ViewMode ViewMode { get; set; }

        public TModel Model { 
            protected get => _model; 
            set
            {
                _model = value;

                _model.IsActiveChanged += () => gameObject.SetActive(_model.IsActive && _model.ViewMode == ViewMode);
                _model.ViewModeChanged += () => gameObject.SetActive(_model.ViewMode == ViewMode);
                _model.PositionChanged += (_) => transform.position = _model.Position;
                _model.AngleChanged += (_) => transform.rotation = Quaternion.Euler(0, 0, _model.Angle);

                gameObject.SetActive(_model.IsActive && _model.ViewMode == ViewMode);
                transform.position = _model.Position;
                transform.rotation = Quaternion.Euler(0, 0, _model.Angle);

                OnModelChanged();
            }
        }

        protected virtual void OnModelChanged()
        {

        }

        protected virtual void OnEnable()
        {
            if (Model is null) return;

            transform.position = _model.Position;
            transform.rotation = Quaternion.Euler(0, 0, _model.Angle);
        }
    }
}
