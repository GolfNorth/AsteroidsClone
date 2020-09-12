using UnityEngine;

namespace AsteroidsClone
{
    public class FireController : Controller, ITickable, IFixedTickable
    {
        private readonly ObjectPool<Bullet> _bulletsPool;
        private float _bulletRapidity;
        private float _bulletDelay;
        private float _laserDuration;
        private float _laserCooldown;
        private float _laserDelay;

        public bool IsLaserReady => !World.Laser.IsActive && _laserDelay >= _laserCooldown;

        public FireController(World world) : base(world)
        {
            var bulletData = (BulletData)World.Data[typeof(BulletData)];
            var laserData = (LaserData)World.Data[typeof(LaserData)];

            _bulletRapidity = bulletData.Rapidity;
            _laserDuration = laserData.Duration;
            _laserCooldown = laserData.Cooldown;

            _bulletsPool = new ObjectPool<Bullet>
            {
                GetInstance = () => { return new Bullet(World); }
            };

            World.Laser = new Laser(world);
            World.Bullets = _bulletsPool.All;
            World.UpdateService.Add(this);
        }

        public override void RestartGame()
        {
            _bulletDelay = 0;
            _laserDelay = 0;

            World.Laser.Disable();

            for (var i = 0; i < World.Bullets.Count; i++)
            {
                _bulletsPool.Release(World.Bullets[i]);
            }
        }

        public void Tick()
        {
            _bulletDelay += Time.deltaTime;
            _laserDelay += Time.deltaTime;

            if (World.Laser.IsActive && World.Ship.IsDestroyed || World.Laser.IsActive && _laserDelay > _laserDuration)
            {
                World.Laser.Disable();

                _laserDelay = 0;
            } 
        }

        public void FixedTick()
        {
            for (var i = 0; i < World.Bullets.Count; i++)
            {
                if (World.Bullets[i] is null || !World.Bullets[i].IsActive) continue;

                World.Bullets[i].FixedTick();
            }

            World.Laser.FixedTick();
        }

        public void Fire(Vector2 position, float angle)
        {
            if (World.Laser.IsActive || _bulletDelay < _bulletRapidity) return;

            var bullet = _bulletsPool.Acquire();

            bullet.Position = position;
            bullet.Angle = angle;

            _bulletDelay = 0;
        }

        public void AltFire(Vector2 position, float angle)
        {
            if (World.Laser.IsActive || _laserDelay < _laserCooldown) return;

            World.Laser.Position = position;
            World.Laser.Angle = angle;
            World.Laser.Enable();

            _laserDelay = 0;
        }

        public void DestroyBullet(Bullet bullet)
        {
            _bulletsPool.Release(bullet);
        }
    }
}