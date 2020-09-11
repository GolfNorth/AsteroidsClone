using UnityEngine;

namespace AsteroidsClone
{
    public class FireController : Resident
    {
        private readonly ObjectPool<Bullet> bullets;
        private readonly Laser laser;

        public FireController(World world) : base(world)
        {
            bullets = new ObjectPool<Bullet>
            {
                GetInstance = () => { return new Bullet(World); }
            };

            laser = new Laser(world);

            World.Bullets = bullets.All;
            World.Laser = laser;
        }

        public void Fire(Vector2 position, float angle)
        {

        }

        public void AltFire(Vector2 position, float angle)
        {

        }
    }
}