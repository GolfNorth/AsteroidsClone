using UnityEngine;

namespace AsteroidsClone
{
    public abstract class View<T> : MonoBehaviour where T : Model
    {
        private T _model;

        public ViewMode ViewMode { get; set; }
        public T Model { 
            protected get => _model; 
            set
            {
                _model = value;

                _model.IsActiveChanged += () => { gameObject.SetActive(_model.ViewMode == ViewMode); };
                _model.ViewModeChanged += () => { gameObject.SetActive(_model.ViewMode == ViewMode); };
                _model.PositionChanged += () => { transform.position = _model.Position; };
                _model.AngleChanged += () => { transform.rotation = Quaternion.Euler(0, 0, _model.Angle); };

                gameObject.SetActive(_model.IsActive);
                transform.position = _model.Position;
                transform.rotation = Quaternion.Euler(0, 0, _model.Angle);
            }
        }

        public virtual void OnEnable()
        {
            if (Model is null) return;

            transform.position = _model.Position;
            transform.rotation = Quaternion.Euler(0, 0, _model.Angle);
        }
    }
}
