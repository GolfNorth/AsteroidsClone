namespace AsteroidsClone
{
    public abstract class Resident
    {
        protected World world;

        protected Resident (World world)
        {
            this.world = world;
        }
    }
}
