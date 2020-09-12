using UnityEngine;

namespace AsteroidsClone
{
    public class FireController : Resident, ITickable, IFixedTickable
    {
        private readonly ObjectPool<Bullet> _bulletsPool;
        private readonly Laser _laser;
        private float _fireRapidity;
        private float _fireDelay;

        public FireController(World world) : base(world)
        {
            _fireRapidity = ((BulletData)World.Data[typeof(BulletData)]).Rapidity;

            _bulletsPool = new ObjectPool<Bullet>
            {
                GetInstance = () => { return new Bullet(World); }
            };

            _laser = new Laser(world);

            World.Bullets = _bulletsPool.All;
            World.Laser = _laser;
            World.UpdateService.Add(this);
        }

        public void Tick()
        {
            _fireDelay += Time.deltaTime;
        }

        public void FixedTick()
        {
            for (var i = 0; i < World.Bullets.Count; i++)
            {
                if (World.Bullets[i] is null || !World.Bullets[i].IsActive) continue;

                World.Bullets[i].FixedTick();
            }
        }

        public void Fire(Vector2 position, float angle)
        {
            if (_fireDelay < _fireRapidity) return;

            var bullet = _bulletsPool.Acquire();

            bullet.Position = position;
            bullet.Angle = angle;

            _fireDelay = 0;
        }

        public void AltFire(Vector2 position, float angle)
        {

        }

        public void DestroyBullet(Bullet bullet)
        {
            _bulletsPool.Release(bullet);
        }
    }
}