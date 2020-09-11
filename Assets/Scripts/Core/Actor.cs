using System;
using UnityEngine;

namespace AsteroidsClone
{
    public abstract class Actor<TModel, TView, TData> : Resident
        where TModel : Model
        where TView : View<TModel>
        where TData : Data
    {
        private readonly TModel _model;
        private readonly TView _polygonView;
        private readonly TView _spriteView;
        private TView _view;
        private ViewMode _viewMode;

        public Actor(World world) : base(world)
        {
            var data = (TData)World.Data[typeof(TData)];

            _model = (TModel)Activator.CreateInstance(typeof(TModel), new object[] { data, World });

            var spriteObject = GameObject.Instantiate(data.SpritePrefab);
            _spriteView = spriteObject.AddComponent<TView>();
            _spriteView.ViewMode = ViewMode.Sprite;
            _spriteView.Model = _model;

            var polygonObject = GameObject.Instantiate(data.PolygonalPrefab);
            _polygonView = polygonObject.AddComponent<TView>();
            _polygonView.ViewMode = ViewMode.Polygonal;
            _polygonView.Model = _model;

            ViewMode = World.ViewMode;
        }

        public TModel Model => _model;

        public TView View => _view;

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
    }
}
