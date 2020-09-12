namespace AsteroidsClone
{
    public abstract class Resident
    {
        private readonly World _world;

        public Resident (World world)
        {
            this._world = world;
        }

        protected World World => _world;
    }
}
