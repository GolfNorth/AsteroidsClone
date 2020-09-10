namespace AsteroidsClone
{
    public class UfosController : Resident, ITickable
    {
        public UfosController(World world) : base(world)
        {
            world.UpdateService.Add(this);
        }

        public void Tick()
        {

        }
    }
}