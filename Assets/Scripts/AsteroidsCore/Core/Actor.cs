using System;

namespace AsteroidsCore
{
    public abstract class Actor<TModel> : Resident, IActor, IPoolable where TModel : Model
    {
        #region Constructor

        protected Actor(World world) : base(world)
        {
            Model = (TModel) Activator.CreateInstance(typeof(TModel), World);

            View = World.ViewFactory.CreateView(Model);
        }

        #endregion

        #region Properties

        public TModel Model { get; }

        public IView<TModel> View { get; set; }

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