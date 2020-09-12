﻿namespace AsteroidsClone
{
    public sealed class Bullet : Actor<BulletModel, BulletView, BulletData>, IFixedTickable
    {
        public Bullet(World world) : base(world)
        {
        }

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
    }
}
