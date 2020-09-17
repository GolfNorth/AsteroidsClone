using System;
using System.Collections.Generic;

namespace AsteroidsCore
{
    public sealed class World : IInitializable, ITickable, ILateTickable, IFixedTickable
    {
        #region Constructor

        public World(float width, float height, float offsetX = 0, float offsetY = 0)
        {
            Width = width;
            Height = height;
            OffsetX = offsetX;
            OffsetY = offsetY;
            
            Random = new Random();
        }

        #endregion

        #region Fields

        public Action OnTick;
        public Action OnLateTick;
        public Action OnFixedTick;

        #endregion

        #region Properties

        public float Width { get; }
        public float Height { get; }
        public float OffsetX { get; }
        public float OffsetY { get; }
        public ViewFactory ViewFactory { get; set; }
        public DataStorage DataStorage { get; set; }
        public Random Random { get; }
        public Ship Ship { get; set; }
        public List<Ufo> Ufos { get; set; }
        public List<Asteroid> Asteroids { get; set; }
        public List<Bullet> Bullets { get; set; }
        public Laser Laser { get; set; }
        public UpdateService UpdateService { get; private set; }
        public PhysicsService PhysicsService { get; private set; }
        public BoundsService BoundsService { get; private set; }
        public IInputService InputService { get; set; }
        public NotificationService NotificationService { get; private set; }
        public ScoreController ScoreController { get; private set; }
        public GameController GameController { get; private set; }
        public AsteroidsController AsteroidsController { get; private set; }
        public UfosController UfosController { get; private set; }
        public FireController FireController { get; private set; }
        public CollisionController CollisionController { get; private set; }

        #endregion

        #region Methods

        public void Initialize()
        {
            InitializeServices();
            InitializeControllers();
        }

        private void InitializeServices()
        {
            UpdateService = new UpdateService(this);
            PhysicsService = new PhysicsService();
            BoundsService = new BoundsService(this);
            NotificationService = new NotificationService();
        }

        private void InitializeControllers()
        {
            ScoreController = new ScoreController(this);
            GameController = new GameController(this);
            AsteroidsController = new AsteroidsController(this);
            UfosController = new UfosController(this);
            FireController = new FireController(this);
            CollisionController = new CollisionController(this);
        }

        public void FixedTick()
        {
            OnFixedTick?.Invoke();
        }

        public void Tick()
        {
            OnTick?.Invoke();
        }

        public void LateTick()
        {
            OnLateTick?.Invoke();
        }

        #endregion
    }
}