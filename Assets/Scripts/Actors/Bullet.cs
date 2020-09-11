using UnityEngine;

namespace AsteroidsClone
{
    public sealed class Bullet : Actor<BulletModel, BulletView, BulletData>, IPoolable, IFixedTickable
    {
        public Bullet(World world) : base(world)
        {
        }

        public bool IsEnabled { get; set; }

        public void Disable()
        {

        }

        public void Enable()
        {

        }

        public void FixedTick()
        {

        }
    }
}
