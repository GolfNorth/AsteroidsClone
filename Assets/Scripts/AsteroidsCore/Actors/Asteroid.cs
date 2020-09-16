namespace AsteroidsCore
{
    public sealed class Asteroid : Actor<AsteroidModel>, IDestroyable, ITickable,
        IFixedTickable
    {
        #region Constructor

        public Asteroid(World world) : base(world)
        {
        }

        #endregion

        #region Properties

        public AsteroidSize Size
        {
            get => Model.Size;
            set => Model.Size = value;
        }

        public CircleShape Shape => Model.Shape;

        public bool IsDestroyed => Model.IsDestroyed;

        #endregion

        #region Methods

        public override void Enable()
        {
            Revive();

            Model.IsActive = true;
        }

        public void Tick()
        {
            if (IsDestroyed && View.IsDestroyed)
                World.AsteroidsController.DestroyAsteroid(this);
        }

        public void FixedTick()
        {
            if (!IsActive || IsDestroyed || Model.Size == AsteroidSize.None) return;

            Model.Move();
        }

        public void Revive()
        {
            Model.Revive();

            World.NotificationService.Notify(NotificationType.AsteroidSpawned, this);
        }

        public void Destroy()
        {
            Model.Destroy();

            World.NotificationService.Notify(NotificationType.AsteroidDestroyed, this);
        }

        public void RandomizeAngleAndSpeed()
        {
            Model.RandomizeAngleAndSpeed();
        }

        public void RandomizePosition()
        {
            Model.RandomizePosition();
        }

        public void RandomizeSize()
        {
            Model.RandomizeSize();
        }

        #endregion
    }
}