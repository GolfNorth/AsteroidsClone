using System;
using System.Collections.Generic;
using UnityEngine;

namespace AsteroidsClone
{
    public sealed class World : MonoBehaviour
    {
        #region Fields

        [SerializeField] private ShipData shipData;
        [SerializeField] private UfoData ufoData;
        [SerializeField] private AsteroidData asteroidData;
        [SerializeField] private BulletData bulletData;
        [SerializeField] private LaserData laserData;
        [SerializeField] private UserInterfaceView userInterfaceView;

        #endregion

        #region Properties

        public ViewMode ViewMode { get; set; }

        public Ship Ship { get; set; }
        public List<Ufo> Ufos { get; set; }
        public List<Asteroid> Asteroids { get; set; }
        public List<Bullet> Bullets { get; set; }
        public Laser Laser { get; set; }

        public Dictionary<Type, Data> Data { get; private set; }

        public UpdateService UpdateService { get; private set; }

        public PhysicsService PhysicsService { get; private set; }

        public BoundsService BoundsService { get; private set; }

        public InputService InputService { get; private set; }

        public NotificationService NotificationService { get; private set; }

        public ScoreController ScoreController { get; private set; }

        public GameController GameController { get; private set; }

        public AsteroidsController AsteroidsController { get; private set; }

        public UfosController UfosController { get; private set; }

        public FireController FireController { get; private set; }

        public ViewModeController ViewModeController { get; private set; }

        public CollisionController CollisionController { get; private set; }

        #endregion

        #region Methods

        private void Awake()
        {
            ViewMode = ViewMode.Sprite;

            InitializeData();
            InitializeServices();
            InitializeControllers();

            userInterfaceView.gameObject.SetActive(true);
        }

        private void InitializeData()
        {
            Data = new Dictionary<Type, Data>
            {
                {typeof(ShipData), shipData},
                {typeof(UfoData), ufoData},
                {typeof(AsteroidData), asteroidData},
                {typeof(BulletData), bulletData},
                {typeof(LaserData), laserData}
            };
        }

        private void InitializeServices()
        {
            UpdateService = new UpdateService();
            PhysicsService = new PhysicsService();
            BoundsService = new BoundsService();
            InputService = new InputService(this);
            NotificationService = new NotificationService();
        }

        private void InitializeControllers()
        {
            ScoreController = new ScoreController(this);
            GameController = new GameController(this);
            AsteroidsController = new AsteroidsController(this);
            UfosController = new UfosController(this);
            FireController = new FireController(this);
            ViewModeController = new ViewModeController(this);
            CollisionController = new CollisionController(this);
        }

        #endregion
    }
}