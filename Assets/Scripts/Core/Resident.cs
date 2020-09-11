namespace AsteroidsClone
{
    public abstract class Resident
    {
        protected World world;

        public Resident (World world)
        {
            this.world = world;
        }
    }
}
