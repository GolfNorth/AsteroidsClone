using UnityEngine;

namespace AsteroidsClone
{
    public class UfosController : Controller, ITickable, IFixedTickable
    {
        private readonly ObjectPool<Ufo> _ufosPool;
        private float _spawnDelay;
        private float _spawnTimer;

        public UfosController(World world) : base(world)
        {
            _spawnDelay = ((UfoData)World.Data[typeof(UfoData)]).SpawnDelay;

            _ufosPool = new ObjectPool<Ufo>
            {
                GetInstance = () => { return new Ufo(World); }
            };

            World.UpdateService.Add(this);
            World.Ufos = _ufosPool.All;
        }

        public override void RestartGame()
        {
            _spawnTimer = 0;

            for (var i = 0; i < World.Ufos.Count; i++)
            {
                _ufosPool.Release(World.Ufos[i]);
            }
        }

        public void Tick()
        {
            for (var i = 0; i < World.Ufos.Count; i++)
            {
                if (World.Ufos[i] is null || !World.Ufos[i].IsActive) continue;

                World.Ufos[i].Tick();
            }

            if (World.Ship.IsDestroyed) return;

            _spawnTimer += Time.deltaTime;

            if (_spawnTimer < _spawnDelay) return;

            SpawnUfo();

            _spawnTimer = 0;
        }

        public void FixedTick()
        {
            for (var i = 0; i < World.Ufos.Count; i++)
            {
                if (World.Ufos[i] is null || !World.Ufos[i].IsActive) continue;

                World.Ufos[i].FixedTick();
            }
        }

        public void SpawnUfo()
        {
            var Ufo = _ufosPool.Acquire();

            Ufo.RandomizePosition();
        }

        public void DestroyUfo(Ufo Ufo)
        {
            _ufosPool.Release(Ufo);
        }
    }
}