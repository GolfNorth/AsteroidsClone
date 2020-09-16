using System;
using System.Collections.Generic;

namespace AsteroidsCore
{
    public sealed class UpdateService : Resident, IDisposable
    {
        #region Constructor

        public UpdateService(World world) : base(world)
        {
            _ticks = new List<ITickable>();
            _lateTicks = new List<ILateTickable>();
            _fixedTicks = new List<IFixedTickable>();
            _disposables = new List<IDisposable>();

            _lastTime = DateTime.Now;
            _fixedLastTime = DateTime.Now;

            World.OnTick += Tick;
            World.OnLateTick += LateTick;
            World.OnFixedTick += FixedTick;
        }

        #endregion

        #region Fields

        private readonly List<ITickable> _ticks;

        private readonly List<ILateTickable> _lateTicks;

        private readonly List<IFixedTickable> _fixedTicks;

        private readonly List<IDisposable> _disposables;

        private DateTime _lastTime;

        private DateTime _fixedLastTime;

        #endregion

        #region Properties

        public float DeltaTime { get; private set; }

        public float FixedDeltaTime { get; private set; }

        #endregion

        #region Methods

        public void Dispose()
        {
            World.OnTick -= Tick;
            World.OnLateTick -= LateTick;
            World.OnFixedTick -= FixedTick;

            for (var i = 0; i < _disposables.Count; i++) _disposables[i]?.Dispose();
        }

        private void Tick()
        {
            var nowTime = DateTime.Now;

            DeltaTime = (float) (nowTime - _lastTime).TotalSeconds;
            _lastTime = nowTime;

            for (var i = 0; i < _ticks.Count; i++) _ticks[i]?.Tick();
        }

        private void FixedTick()
        {
            var nowTime = DateTime.Now;

            FixedDeltaTime = (float) (nowTime - _fixedLastTime).TotalSeconds;
            _fixedLastTime = nowTime;

            for (var i = 0; i < _fixedTicks.Count; i++) _fixedTicks[i]?.FixedTick();
        }

        private void LateTick()
        {
            for (var i = 0; i < _lateTicks.Count; i++) _lateTicks[i]?.LateTick();
        }

        public void Add(object updatable)
        {
            if (updatable is IInitializable initializable) initializable.Initialize();

            if (updatable is ITickable tick) _ticks.Add(tick);

            if (updatable is ILateTickable lateTick) _lateTicks.Add(lateTick);

            if (updatable is IFixedTickable fixedTick) _fixedTicks.Add(fixedTick);

            if (updatable is IDisposable disposable) _disposables.Add(disposable);
        }

        public void Remove(object updatable)
        {
            if (updatable is ITickable tick) _ticks.Remove(tick);

            if (updatable is ILateTickable lateTick) _lateTicks.Remove(lateTick);

            if (updatable is IFixedTickable fixedTick) _fixedTicks.Remove(fixedTick);

            if (updatable is IDisposable disposable) _disposables.Remove(disposable);
        }

        #endregion
    }
}