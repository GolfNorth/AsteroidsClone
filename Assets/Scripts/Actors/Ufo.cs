using UnityEngine;

namespace AsteroidsClone
{
    public sealed class Ufo : Actor<UfoModel, UfoView, UfoData>, IPoolable, IFixedTickable
    {
        public Ufo(World world) : base(world)
        {
        }

        public bool IsEnabled { get; set; }

        public void Disable()
        {

        }

        public void Enable()
        {

        }

        public void FixedTick()
        {

        }
    }
}
