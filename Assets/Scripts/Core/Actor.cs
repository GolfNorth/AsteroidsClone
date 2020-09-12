using System;
using UnityEngine;

namespace AsteroidsClone
{
    public abstract class Actor<TModel, TView, TData> : Resident, IPoolable, IActivatable
        where TModel : Model
        where TView : View<TModel>
        where TData : Data
    {
        private readonly TModel _model;
        private readonly TData _data;
        private readonly TView _polygonView;
        private readonly TView _spriteView;
        private TView _view;
        private ViewMode _viewMode;

        public Actor(World world) : base(world)
        {
            _data = (TData)World.Data[typeof(TData)];
            _model = (TModel)Activator.CreateInstance(typeof(TModel), new object[] { Data, World });

            _spriteView = CreateView(ViewMode.Sprite);
            _polygonView = CreateView(ViewMode.Polygonal);

            ViewMode = World.ViewMode;
        }

        private TView CreateView(ViewMode viewMode)
        {
            var gameObject = viewMode == ViewMode.Polygonal
                ? GameObject.Instantiate(Data.SpritePrefab)
                : GameObject.Instantiate(Data.PolygonalPrefab);

            gameObject.name = this.GetType().Name;

            var view = gameObject.AddComponent<TView>();
            view.ViewMode = viewMode;
            view.Model = Model;

            return view;
        }

        public virtual void Enable()
        {
            Model.IsActive = true;
        }

        public virtual void Disable()
        {
            Model.IsActive = false;
        }

        protected TModel Model => _model;

        protected TData Data => _data;

        protected TView View => _view;

        public ViewMode ViewMode
        {
            get => _viewMode;
            set
            {
                if (_viewMode == value) return;

                _viewMode = value;
                _model.ViewMode = ViewMode;
                _view = value == ViewMode.Polygonal ? _polygonView : _spriteView;
            }
        }

        public bool IsActive
        {
            get => _model.IsActive;
            set => _model.IsActive = value;
        }

        public Vector2 Position
        {
            get => _model.Position;
            set => _model.Position = value;
        }

        public float Angle
        {
            get => _model.Angle;
            set => _model.Angle = value;
        }
    }
}
