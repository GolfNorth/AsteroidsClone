using UnityEngine;

namespace AsteroidsClone
{
    public class FireController : Resident, ITickable, IFixedTickable
    {
        private readonly ObjectPool<Bullet> _bulletsPool;
        private readonly Laser _laser;
        private float _bulletRapidity;
        private float _bulletDelay;
        private float _laserDuration;
        private float _laserCooldown;
        private float _laserDelay;

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

            _laser = new Laser(world);

            World.Bullets = _bulletsPool.All;
            World.Laser = _laser;
            World.UpdateService.Add(this);
        }

        public void Tick()
        {
            _bulletDelay += Time.deltaTime;
            _laserDelay += Time.deltaTime;

            if (_laser.IsActive && _laserDelay > _laserDuration)
            {
                _laser.Disable();

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

            _laser.FixedTick();
        }

        public void Fire(Vector2 position, float angle)
        {
            if (_laser.IsActive || _bulletDelay < _bulletRapidity) return;

            var bullet = _bulletsPool.Acquire();

            bullet.Position = position;
            bullet.Angle = angle;

            _bulletDelay = 0;
        }

        public void AltFire(Vector2 position, float angle)
        {
            if (_laser.IsActive || _laserDelay < _laserCooldown) return;

            _laser.Position = position;
            _laser.Angle = angle;
            _laser.Enable();

            _laserDelay = 0;
        }

        public void DestroyBullet(Bullet bullet)
        {
            _bulletsPool.Release(bullet);
        }
    }
}