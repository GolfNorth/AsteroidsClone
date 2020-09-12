using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace AsteroidsClone
{
    public abstract class Actor<TModel, TView, TData> : Resident, IPoolable
        where TModel : Model
        where TView : View<TModel>
        where TData : Data
    {
        #region Constructor

        protected Actor(World world) : base(world)
        {
            Data = (TData) World.Data[typeof(TData)];
            Model = (TModel) Activator.CreateInstance(typeof(TModel), Data, World);

            _spriteView = CreateView(ViewMode.Sprite);
            _polygonView = CreateView(ViewMode.Polygonal);

            ViewMode = World.ViewMode;
        }

        #endregion

        #region Methods

        private TView CreateView(ViewMode viewMode)
        {
            var gameObject = viewMode == ViewMode.Polygonal
                ? Object.Instantiate(Data.SpritePrefab)
                : Object.Instantiate(Data.PolygonalPrefab);

            gameObject.name = GetType().Name;

            var view = gameObject.AddComponent<TView>();
            view.ViewMode = viewMode;
            view.Model = Model;

            return view;
        }

        #endregion

        #region Fields

        private readonly TView _polygonView;
        private readonly TView _spriteView;
        private ViewMode _viewMode;

        #endregion

        #region Properties

        protected TModel Model { get; }

        protected TData Data { get; }

        protected TView View { get; private set; }

        public ViewMode ViewMode
        {
            get => _viewMode;
            set
            {
                if (_viewMode == value) return;

                _viewMode = value;
                Model.ViewMode = ViewMode;
                View = value == ViewMode.Polygonal ? _polygonView : _spriteView;
            }
        }

        public Vector2 Position
        {
            get => Model.Position;
            set => Model.Position = value;
        }

        public float Angle
        {
            get => Model.Angle;
            set => Model.Angle = value;
        }

        public virtual void Enable()
        {
            Model.IsActive = true;
        }

        public virtual void Disable()
        {
            Model.IsActive = false;
        }

        public bool IsActive
        {
            get => Model.IsActive;
            set => Model.IsActive = value;
        }

        #endregion
    }
}