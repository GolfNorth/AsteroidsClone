namespace AsteroidsCore
{
    public sealed class Ship : Actor<ShipModel>, IDestroyable, ITickable, IFixedTickable
    {
        #region Constructor

        public Ship(World world) : base(world)
        {
        }

        #endregion

        #region Properties

        public PolygonShape Shape => Model.Shape;

        public bool IsDestroyed => Model.IsDestroyed;

        #endregion

        #region Properties

        public void Tick()
        {
            if (IsDestroyed)
            {
                if (View.IsDestroyed) Model.IsActive = false;

                return;
            }

            if (World.InputService.Fire) Fire();

            if (World.InputService.AltFire) AltFire();
        }

        public void FixedTick()
        {
            if (!IsActive || IsDestroyed) return;

            Model.Move(World.InputService.Translation, World.InputService.Rotation);
        }

        private void Fire()
        {
            if (!IsActive || IsDestroyed) return;

            World.FireController.Fire(Model.Position, Model.Angle);
        }

        private void AltFire()
        {
            if (!IsActive || IsDestroyed) return;

            World.FireController.AltFire(Model.Position, Model.Angle);
        }

        public override void Enable()
        {
            Revive();

            Model.IsActive = true;
        }

        public void Revive()
        {
            Model.Revive();

            World.NotificationService.Notify(NotificationType.ShipSpawned, this);
        }

        public void Destroy()
        {
            Model.Destroy();

            World.NotificationService.Notify(NotificationType.ShipDestroyed, this);
        }

        #endregion
    }
}