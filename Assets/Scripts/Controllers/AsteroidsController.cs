using UnityEngine;

namespace AsteroidsClone
{
    public class AsteroidsController : Resident, ITickable, IFixedTickable
    {
        private readonly ObjectPool<Asteroid> _asteroidsPool;
        private float _spawnDelay;
        private float _spawnTimer;

        public AsteroidsController(World world) : base(world)
        {
            _spawnDelay = ((AsteroidData) World.Data[typeof(AsteroidData)]).SpawnDelay;

            _asteroidsPool = new ObjectPool<Asteroid>
            {
                GetInstance = () => { return new Asteroid(World); }
            };

            SpawnAsteroid();

            World.UpdateService.Add(this);
            World.Asteroids = _asteroidsPool.All;
        }

        public void Tick()
        {
            for (var i = 0; i < World.Asteroids.Count; i++)
            {
                if (World.Asteroids[i] is null || !World.Asteroids[i].IsActive) continue;

                World.Asteroids[i].Tick();
            }

            _spawnTimer += Time.deltaTime;

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

        public void SpawnAsteroid(AsteroidSize size = AsteroidSize.None, Vector2 position = new Vector2())
        {
            var asteroid = _asteroidsPool.Acquire();

            if (size == AsteroidSize.None)
                asteroid.RandomizeSize();
            else
                asteroid.Size = size;

            if (position == Vector2.zero)
                asteroid.RandomizePosition();
            else
                asteroid.Position = position;

            asteroid.RandomizeAngleAndSpeed();
        }

        public void DestroyAsteroid(Asteroid asteroid)
        {
            if (asteroid.Size == AsteroidSize.Large)
            {
                SpawnAsteroid(AsteroidSize.Middle, asteroid.Position);
                SpawnAsteroid(AsteroidSize.Middle, asteroid.Position);
            } else if (asteroid.Size == AsteroidSize.Middle)
            {
                SpawnAsteroid(AsteroidSize.Small, asteroid.Position);
                SpawnAsteroid(AsteroidSize.Small, asteroid.Position);
            }
            
            _asteroidsPool.Release(asteroid);
        }
    }
}