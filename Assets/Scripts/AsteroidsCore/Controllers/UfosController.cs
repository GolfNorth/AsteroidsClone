namespace AsteroidsCore
{
    public sealed class UfosController : Controller, ITickable, IFixedTickable
    {
        #region Constructor

        public UfosController(World world) : base(world)
        {
            _spawnDelay = World.DataStorage.UfoData.spawnDelay;

            _ufosPool = new ObjectPool<Ufo>
            {
                GetInstance = () => new Ufo(World)
            };

            World.UpdateService.Add(this);
            World.Ufos = _ufosPool.All;
        }

        #endregion

        #region Fields

        private readonly ObjectPool<Ufo> _ufosPool;
        private readonly float _spawnDelay;
        private float _spawnTimer;

        #endregion

        #region Methods

        public void FixedTick()
        {
            for (var i = 0; i < World.Ufos.Count; i++)
            {
                if (World.Ufos[i] is null || !World.Ufos[i].IsActive) continue;

                World.Ufos[i].FixedTick();
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

            _spawnTimer += World.UpdateService.DeltaTime;

            if (_spawnTimer < _spawnDelay) return;

            SpawnUfo();

            _spawnTimer = 0;
        }

        public override void RestartGame()
        {
            _spawnTimer = 0;

            for (var i = 0; i < World.Ufos.Count; i++) _ufosPool.Release(World.Ufos[i]);
        }

        private void SpawnUfo()
        {
            var ufo = _ufosPool.Acquire();

            ufo.RandomizePosition();
        }

        public void DestroyUfo(Ufo ufo)
        {
            _ufosPool.Release(ufo);
        }

        #endregion
    }
}