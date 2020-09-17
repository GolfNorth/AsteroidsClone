using AsteroidsCore;
using UnityEngine;

namespace AsteroidsView
{
    public sealed class UnityInputService : IInputService, ITickable
    {
        #region Methods

        public void Tick()
        {
            Translation = Input.GetAxis("Vertical");
            Rotation = Input.GetAxis("Horizontal");
            Fire = Input.GetButton("Fire1");
            AltFire = Input.GetButton("Fire2");
        }

        #endregion

        #region Properties

        public float Translation { get; private set; }

        public float Rotation { get; private set; }

        public bool Fire { get; private set; }

        public bool AltFire { get; private set; }

        #endregion
    }
}