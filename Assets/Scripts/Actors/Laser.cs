using UnityEngine;

namespace AsteroidsClone
{
    public sealed class Laser : Actor<LaserModel, LaserView, LaserData>, IFixedTickable
    {
        public Laser(World world) : base(world)
        {
        }

        public void FixedTick()
        {

        }
    }
}
