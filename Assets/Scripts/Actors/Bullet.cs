namespace AsteroidsClone
{
    public sealed class Bullet : Actor<BulletModel, BulletView, BulletData>, IFixedTickable
    {
        #region Constructor

        public Bullet(World world) : base(world)
        {
        }

        #endregion

        #region Methods

        public override void Enable()
        {
            Model.IsActive = true;

            World.NotificationService.Notify(NotificationType.BulletFired);
        }

        public void FixedTick()
        {
            if (!IsActive) return;

            Model.Move();

            if (Model.IsInsideField) return;

            World.FireController.DestroyBullet(this);
        }

        #endregion
    }
}