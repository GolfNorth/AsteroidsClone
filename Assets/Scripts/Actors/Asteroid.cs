namespace AsteroidsClone
{
    public sealed class Asteroid : Actor<AsteroidModel, AsteroidView, AsteroidData>, IDestroyable, IPoolable, IFixedTickable
    {
        public Asteroid(World world) : base(world)
        {
        }

        public AsteroidSize Size
        {
            get => Model.Size;
            set => Model.Size = value;
        }

        public CircleShape Shape => Model.Shape;

        public void Disable()
        {
            Model.IsActive = false;
        }

        public void Enable()
        {
            Model.IsActive = true;
        }

        public void FixedTick()
        {
            if (!IsActive || Model.Size == AsteroidSize.None) return;

            Model.Move();
        }

        public void Destroy()
        {
            World.AsteroidsController.DestroyAsteroid(this);
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
