using UnityEngine;

namespace AsteroidsClone
{
    public sealed class Ship : Actor<ShipModel, ShipView, ShipData>, IFixedTickable
    {
        public Ship(World world) : base(world)
        {
        }

        public void FixedTick()
        {

        }
    }
}
