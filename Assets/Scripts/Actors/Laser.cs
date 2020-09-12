namespace AsteroidsClone
{
    public sealed class Laser : Actor<LaserModel, LaserView, LaserData>, IFixedTickable
    {
        public LineShape Shape => Model.Shape;

        public Laser(World world) : base(world)
        {
        }

        public override void Enable()
        {
            base.Enable();

            World.NotificationService.Notify(NotificationType.LaserActivated, this);
        }

        public override void Disable()
        {
            base.Disable();

            World.NotificationService.Notify(NotificationType.LaserDeactivated, this);
        }

        public void FixedTick()
        {
            if (!IsActive) return;

            Model.Move();
        }
    }
}
