namespace AsteroidsClone
{
    public abstract class Resident
    {
        private readonly World world;

        public Resident (World world)
        {
            this.world = world;
        }

        protected World World => world;
    }
}
