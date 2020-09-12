using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsteroidsClone
{
    public sealed class UpdateService
    {
        #region Fields

        private readonly UpdateServiceComponent _component;

        #endregion

        #region Constructor

        public UpdateService()
        {
            var go = new GameObject("UpdateService");

            _component = go.AddComponent<UpdateServiceComponent>();
        }

        #endregion

        #region Private Objects

        private sealed class UpdateServiceComponent : MonoBehaviour
        {
            public List<ITickable> Ticks { get; } = new List<ITickable>();

            public List<ILateTickable> LateTicks { get; } = new List<ILateTickable>();

            public List<IFixedTickable> FixedTicks { get; } = new List<IFixedTickable>();

            public List<IDisposable> Disposables { get; } = new List<IDisposable>();

            private void Update()
            {
                for (var i = 0; i < Ticks.Count; i++) Ticks[i]?.Tick();
            }

            private void FixedUpdate()
            {
                for (var i = 0; i < FixedTicks.Count; i++) FixedTicks[i]?.FixedTick();
            }

            private void LateUpdate()
            {
                for (var i = 0; i < LateTicks.Count; i++) LateTicks[i]?.LateTick();
            }

            private void OnDestroy()
            {
                for (var i = 0; i < Disposables.Count; i++) Disposables[i]?.Dispose();
            }

            public void StartNewCoroutine(IEnumerator method)
            {
                StartCoroutine(method);
            }
        }

        #endregion

        #region Methods

        public void Add(object updatable)
        {
            if (updatable is IInitializable initializable) initializable.Initialize();

            if (updatable is ITickable tick) _component.Ticks.Add(tick);

            if (updatable is ILateTickable lateTick) _component.LateTicks.Add(lateTick);

            if (updatable is IFixedTickable fixedTick) _component.FixedTicks.Add(fixedTick);

            if (updatable is IDisposable disposable) _component.Disposables.Add(disposable);
        }

        public void Remove(object updatable)
        {
            if (updatable is ITickable tick) _component.Ticks.Remove(tick);

            if (updatable is ILateTickable lateTick) _component.LateTicks.Remove(lateTick);

            if (updatable is IFixedTickable fixedTick) _component.FixedTicks.Remove(fixedTick);

            if (updatable is IDisposable disposable) _component.Disposables.Remove(disposable);
        }

        public void StartCoroutine(IEnumerator method)
        {
            _component.StartNewCoroutine(method);
        }

        #endregion
    }
}