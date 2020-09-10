using UnityEngine;

namespace AsteroidsClone
{
    public class AsteroidsController : Resident, ITickable
    {
        public AsteroidsController(World world) : base(world)
        {
            world.UpdateService.Add(this);
        }

        public void Tick()
        {
        }
    }
}