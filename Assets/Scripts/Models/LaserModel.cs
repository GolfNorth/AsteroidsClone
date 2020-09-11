using UnityEngine;

namespace AsteroidsClone
{
    public sealed class LaserModel : Model
    {
        public LaserModel(LaserData data, World world) : base(world)
        {
            Data = data;
        }

        private LaserData Data { get; set; }
    }
}
