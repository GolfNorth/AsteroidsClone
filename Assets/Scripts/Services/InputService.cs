using UnityEngine;

namespace AsteroidsClone
{
    public sealed class InputService : ITickable
    {
        private float _translation;
        private float _rotation;
        private bool _fire;
        private bool _altFire;

        public float Translation => _translation;
        public float Rotation => _rotation;
        public bool Fire => _fire;
        public bool AltFire => _altFire;

        public InputService(World world)
        {
            world.UpdateService.Add(this);
        }

        public void Tick()
        {
            _translation = Input.GetAxis("Vertical");
            _rotation = Input.GetAxis("Horizontal");
            _fire = Input.GetButton("Fire1");
            _altFire = Input.GetButton("Fire2");
        }
    }
}