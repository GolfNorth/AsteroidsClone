namespace AsteroidsCore
{
    public sealed class Ufo : Actor<UfoModel>, IDestroyable, ITickable, IFixedTickable
    {
        #region Constructor

        public Ufo(World world) : base(world)
        {
        }

        #endregion

        #region Properties

        public PolygonShape Shape => Model.Shape;

        public bool IsDestroyed => Model.IsDestroyed;

        #endregion

        #region Methods

        public override void Enable()
        {
            Revive();

            Model.IsActive = true;
        }

        public void Tick()
        {
            if (IsDestroyed && View.IsDestroyed)
                World.UfosController.DestroyUfo(this);
        }

        public void FixedTick()
        {
            if (!IsActive || IsDestroyed || World.Ship.IsDestroyed) return;

            Model.Move();
        }

        public void Revive()
        {
            Model.Revive();

            World.NotificationService.Notify(NotificationType.UfoSpawned, this);
        }

        public void Destroy()
        {
            Model.Destroy();

            World.NotificationService.Notify(NotificationType.UfoDestroyed, this);
        }

        public void RandomizePosition()
        {
            Model.RandomizePosition();
        }

        #endregion
    }
}