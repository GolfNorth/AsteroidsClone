namespace AsteroidsClone
{
    public sealed class Ship : Actor<ShipModel, ShipView, ShipData>, IDestroyable, ITickable, IFixedTickable
    {
        public PolygonShape Shape => Model.Shape;

        public bool IsDestroyed => Model.IsDestroyed;

        public Ship(World world) : base(world)
        {
        }

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
            if (IsDestroyed) return;

            Model.Move(World.InputService.Translation, World.InputService.Rotation);
        }

        public void Fire()
        {
            World.FireController.Fire(Model.Position, Model.Angle);
        }

        public void AltFire()
        {
            World.FireController.Fire(Model.Position, Model.Angle);
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
    }
}
