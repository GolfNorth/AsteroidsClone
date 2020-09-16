namespace AsteroidsCore
{
    public sealed class FireController : Controller, ITickable, IFixedTickable
    {
        #region Constructor

        public FireController(World world) : base(world)
        {
            var bulletData = World.DataStorage.BulletData;
            var laserData = World.DataStorage.LaserData;

            _bulletRapidity = bulletData.rapidity;
            _laserDuration = laserData.duration;
            _laserCooldown = laserData.cooldown;

            _bulletsPool = new ObjectPool<Bullet>
            {
                GetInstance = () => new Bullet(World)
            };

            World.Laser = new Laser(world);
            World.Bullets = _bulletsPool.All;
            World.UpdateService.Add(this);
        }

        #endregion

        #region Properties

        public bool IsLaserReady => !World.Laser.IsActive && _laserDelay >= _laserCooldown;

        #endregion

        #region Fields

        private readonly ObjectPool<Bullet> _bulletsPool;
        private float _bulletDelay;
        private readonly float _bulletRapidity;
        private readonly float _laserCooldown;
        private float _laserDelay;
        private readonly float _laserDuration;

        #endregion

        #region Methods

        public void FixedTick()
        {
            for (var i = 0; i < World.Bullets.Count; i++)
            {
                if (World.Bullets[i] is null || !World.Bullets[i].IsActive) continue;

                World.Bullets[i].FixedTick();
            }

            World.Laser.FixedTick();
        }

        public void Tick()
        {
            _bulletDelay += World.UpdateService.DeltaTime;
            _laserDelay += World.UpdateService.DeltaTime;

            if (World.Laser.IsActive && World.Ship.IsDestroyed || World.Laser.IsActive && _laserDelay > _laserDuration)
            {
                World.Laser.Disable();

                _laserDelay = 0;
            }
        }

        public override void RestartGame()
        {
            _bulletDelay = 0;
            _laserDelay = 0;

            World.Laser.Disable();

            for (var i = 0; i < World.Bullets.Count; i++) _bulletsPool.Release(World.Bullets[i]);
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

        #endregion
    }
}