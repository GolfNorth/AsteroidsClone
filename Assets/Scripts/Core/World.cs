using System;
using System.Collections;
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

        private UpdateService _updateService;
        private PhysicsService _physicsService;
        private BoundsService _boundsService;
        private NotificationService _notificationService;
        private InputService _inputService;

        private ScoreController _scoreController;
        private GameController _gameController;
        private AsteroidsController _asteroidsController;
        private UfosController _ufosController;
        private FireController _fireController;
        private CollisionController _collisionController;

        public ViewMode ViewMode { get; set; }

        public Ship Ship { get; set; }
        public List<Ufo> Ufos { get; set; }
        public List<Asteroid> Asteroids { get; set; }
        public List<Bullet> Bullets { get; set; }
        public Laser Laser { get; set; }

        public Dictionary<Type, Data> Data { get; private set; }

        public UpdateService UpdateService => _updateService;
        public PhysicsService PhysicsService => _physicsService;
        public BoundsService BoundsService => _boundsService;
        public InputService InputService => _inputService;
        public NotificationService NotificationService => _notificationService;

        public ScoreController ScoreController => _scoreController;
        public GameController GameController => _gameController;
        public AsteroidsController AsteroidsController => _asteroidsController;
        public UfosController UfosController => _ufosController;
        public FireController FireController => _fireController;
        public CollisionController CollisionController => _collisionController;

        private void Awake()
        {
            ViewMode = ViewMode.Sprite;

            InitializeData();
            InitializeServices();
            InitializeControllers();
        }

        private void InitializeData()
        {
            Data = new Dictionary<Type, Data>();

            Data.Add(typeof(ShipData), shipData);
            Data.Add(typeof(UfoData), ufoData);
            Data.Add(typeof(AsteroidData), asteroidData);
            Data.Add(typeof(BulletData), bulletData);
            Data.Add(typeof(LaserData), laserData);
        }

        private void InitializeServices()
        {
            _updateService = new UpdateService();
            _physicsService = new PhysicsService();
            _boundsService = new BoundsService();
            _inputService = new InputService(this);
            _notificationService = new NotificationService();
        }

        private void InitializeControllers()
        {
            _scoreController = new ScoreController(this);
            _gameController = new GameController(this);
            _asteroidsController = new AsteroidsController(this);
            _ufosController = new UfosController(this);
            _fireController = new FireController(this);
            _collisionController = new CollisionController(this);
        }
    }
}
