namespace AsteroidsCore
{
    public sealed class Laser : Actor<LaserModel>, IFixedTickable
    {
        #region Constructor

        public Laser(World world) : base(world)
        {
        }

        #endregion

        #region Properties

        public LineShape Shape => Model.Shape;

        #endregion

        #region Methods

        public override void Enable()
        {
            Model.IsActive = true;

            World.NotificationService.Notify(NotificationType.LaserActivated, this);
        }

        public override void Disable()
        {
            Model.IsActive = false;

            World.NotificationService.Notify(NotificationType.LaserDeactivated, this);
        }

        public void FixedTick()
        {
            if (!IsActive) return;

            Model.Move();
        }

        #endregion
    }
}