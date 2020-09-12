namespace AsteroidsClone
{
    public abstract class Controller : Resident
    {
        public Controller(World world) : base(world)
        {
        }

        public virtual void RestartGame()
        {

        }
    }
}