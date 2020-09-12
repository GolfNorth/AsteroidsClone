namespace AsteroidsClone
{
    public sealed class Asteroid : Actor<AsteroidModel, AsteroidView, AsteroidData>, IDestroyable, IPoolable, ITickable, IFixedTickable
    {
        public AsteroidSize Size
        {
            get => Model.Size;
            set => Model.Size = value;
        }

        public CircleShape Shape => Model.Shape;

        public bool IsDestroyed => Model.IsDestroyed;

        public Asteroid(World world) : base(world)
        {
        }

        public void Disable()
        {
            Model.IsActive = false;
        }

        public void Enable()
        {
            Model.IsActive = true;

            Revive();
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
    }
}
