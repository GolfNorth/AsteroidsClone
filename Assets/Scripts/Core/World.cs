using System.Collections.Generic;
using UnityEngine;

namespace AsteroidsClone
{
    public sealed class World : MonoBehaviour
    {
        [SerializeField] private ShipData shipData;
        [SerializeField] private UfoData ufoData;
        [SerializeField] private AsteroidData asteroidData;
        [SerializeField] private BulletData bulletData;
        [SerializeField] private LaserData laserData;

        private Ship _ship;
        private List<Ufo> _ufos;
        private List<Asteroid> _asteroids;
        private List<Bullet> _bullets;
        private Laser _laser;

        private UpdateService _updateService;
        private BoundsService _boundsService;
        private NotificationService _notificationService;

        private AsteroidsController _asteroidsController;
        private UfosController _ufosController;
        private BulletsController _bulletsController;

        public Ship Ship { get => _ship; set => _ship = value; }
        public List<Ufo> Ufos { get => _ufos; set => _ufos = value; }
        public List<Asteroid> Asteroids { get => _asteroids; set => _asteroids = value; }
        public List<Bullet> Bullets { get => _bullets; set => _bullets = value; }
        public Laser Laser { get => _laser; set => _laser = value; }

        public UpdateService UpdateService => _updateService;
        public BoundsService BoundsService => _boundsService;
        public NotificationService NotificationService => _notificationService;

        public AsteroidsController AsteroidsController { get => _asteroidsController; set => _asteroidsController = value; }
        public UfosController UfosController { get => _ufosController; set => _ufosController = value; }
        public BulletsController BulletsController { get => _bulletsController; set => _bulletsController = value; }

        private void Awake()
        {
            _updateService = new UpdateService();
            _boundsService = new BoundsService();
            _notificationService = new NotificationService();
        }
    }
}
