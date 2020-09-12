using UnityEngine;

namespace AsteroidsClone
{
    public class FireController : Resident
    {
        private readonly ObjectPool<Bullet> _bullets;
        private readonly Laser _laser;

        public FireController(World world) : base(world)
        {
            _bullets = new ObjectPool<Bullet>
            {
                GetInstance = () => { return new Bullet(World); }
            };

            _laser = new Laser(world);

            World.Bullets = _bullets.All;
            World.Laser = _laser;
        }

        public void Fire(Vector2 position, float angle)
        {

        }

        public void AltFire(Vector2 position, float angle)
        {

        }
    }
}