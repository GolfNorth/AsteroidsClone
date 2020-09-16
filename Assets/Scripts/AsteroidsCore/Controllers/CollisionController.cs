namespace AsteroidsCore
{
    public sealed class CollisionController : Controller, IFixedTickable
    {
        #region Constructor

        public CollisionController(World world) : base(world)
        {
            World.UpdateService.Add(this);
        }

        #endregion

        #region Methods

        public void FixedTick()
        {
            var ship = World.Ship;

            if (!ship.IsActive || ship.IsDestroyed) return;

            int a, b, u;

            for (b = 0; b < World.Bullets.Count; b++)
            {
                var bullet = World.Bullets[b];

                for (a = 0; a < World.Asteroids.Count; a++)
                {
                    var asteroid = World.Asteroids[a];

                    if (asteroid.IsActive && !asteroid.IsDestroyed &&
                        World.PhysicsService.PointAndCircleContact(bullet.Position, asteroid.Shape))
                    {
                        asteroid.Destroy();
                        World.FireController.DestroyBullet(bullet);

                        break;
                    }
                }

                if (!bullet.IsActive) continue;

                for (u = 0; u < World.Ufos.Count; u++)
                {
                    var ufo = World.Ufos[u];

                    if (ufo.IsActive && !ufo.IsDestroyed &&
                        World.PhysicsService.PolygonAndPointContact(ufo.Shape, bullet.Position))
                    {
                        ufo.Destroy();
                        World.FireController.DestroyBullet(bullet);

                        break;
                    }
                }
            }

            var laser = World.Laser;

            if (laser.IsActive)
            {
                for (a = 0; a < World.Asteroids.Count; a++)
                {
                    var asteroid = World.Asteroids[a];

                    if (asteroid.IsActive && !asteroid.IsDestroyed &&
                        World.PhysicsService.LineAndCircleContact(laser.Shape, asteroid.Shape))
                        asteroid.Destroy();
                }

                for (u = 0; u < World.Ufos.Count; u++)
                {
                    var ufo = World.Ufos[u];

                    if (ufo.IsActive && !ufo.IsDestroyed &&
                        World.PhysicsService.PolygonAndLineContact(ufo.Shape, laser.Shape))
                        ufo.Destroy();
                }
            }

            for (a = 0; a < World.Asteroids.Count; a++)
            {
                var asteroid = World.Asteroids[a];

                if (asteroid.IsActive && !asteroid.IsDestroyed &&
                    World.PhysicsService.PolygonAndCircleContact(ship.Shape, asteroid.Shape))
                {
                    asteroid.Destroy();
                    ship.Destroy();

                    break;
                }
            }

            if (ship.IsActive && !ship.IsDestroyed)
                for (u = 0; u < World.Ufos.Count; u++)
                {
                    var ufo = World.Ufos[u];

                    if (ufo.IsActive && !ufo.IsDestroyed &&
                        World.PhysicsService.PolygonAndPolygonContact(ship.Shape, ufo.Shape))
                    {
                        ufo.Destroy();
                        ship.Destroy();

                        break;
                    }
                }
        }

        #endregion
    }
}