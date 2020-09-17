using System.Numerics;

namespace AsteroidsCore
{
    public sealed class AsteroidsController : Controller, ITickable, IFixedTickable
    {
        #region Constructor

        public AsteroidsController(World world) : base(world)
        {
            _spawnDelay = World.DataStorage.AsteroidData.spawnDelay;

            _asteroidsPool = new ObjectPool<Asteroid>
            {
                GetInstance = () => new Asteroid(World)
            };

            SpawnAsteroid();

            World.Asteroids = _asteroidsPool.All;
            World.NotificationService.Notification += SpawnSmaller;

            World.UpdateService.Add(this);
        }

        #endregion

        #region Fields

        private readonly ObjectPool<Asteroid> _asteroidsPool;
        private readonly float _spawnDelay;
        private float _spawnTimer;

        #endregion

        #region Methods

        public override void RestartGame()
        {
            _spawnTimer = 0;

            for (var i = 0; i < World.Asteroids.Count; i++)
                _asteroidsPool.Release(World.Asteroids[i]);
        }

        public void Tick()
        {
            for (var i = 0; i < World.Asteroids.Count; i++)
            {
                if (World.Asteroids[i] is null || !World.Asteroids[i].IsActive) continue;

                World.Asteroids[i].Tick();
            }

            if (World.Ship.IsDestroyed) return;

            _spawnTimer += World.UpdateService.DeltaTime;

            if (_spawnTimer < _spawnDelay) return;

            SpawnAsteroid();

            _spawnTimer = 0;
        }

        public void FixedTick()
        {
            for (var i = 0; i < World.Asteroids.Count; i++)
            {
                if (World.Asteroids[i] is null || !World.Asteroids[i].IsActive) continue;

                World.Asteroids[i].FixedTick();
            }
        }

        private void SpawnAsteroid(AsteroidSize size = AsteroidSize.None, Vector2 position = new Vector2())
        {
            var asteroid = _asteroidsPool.Acquire();

            if (size == AsteroidSize.None)
                asteroid.RandomizeSize();
            else
                asteroid.Size = size;

            if (position == Vector2.Zero)
                asteroid.RandomizePosition();
            else
                asteroid.Position = position;

            asteroid.RandomizeAngleAndSpeed();
        }

        private void SpawnSmaller(NotificationType notificationType, object obj)
        {
            if (notificationType != NotificationType.AsteroidDestroyed) return;

            var asteroid = (Asteroid) obj;

            if (asteroid.Size == AsteroidSize.Large)
            {
                SpawnAsteroid(AsteroidSize.Middle, asteroid.Position);
                SpawnAsteroid(AsteroidSize.Middle, asteroid.Position);
            }
            else if (asteroid.Size == AsteroidSize.Middle)
            {
                SpawnAsteroid(AsteroidSize.Small, asteroid.Position);
                SpawnAsteroid(AsteroidSize.Small, asteroid.Position);
            }
        }

        public void DestroyAsteroid(Asteroid asteroid)
        {
            _asteroidsPool.Release(asteroid);
        }

        #endregion
    }
}