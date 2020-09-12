namespace AsteroidsClone
{
    public sealed class Ufo : Actor<UfoModel, UfoView, UfoData>, IDestroyable, IPoolable, ITickable, IFixedTickable
    {
        public PolygonShape Shape => Model.Shape;

        public bool IsDestroyed => Model.IsDestroyed;

        public Ufo(World world) : base(world)
        {
        }

        public override void Enable()
        {
            Revive();

            base.Enable();
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
    }
}
