using UnityEngine;

namespace AsteroidsClone
{
    public sealed class UfoModel : Model
    {
        public UfoModel(UfoData data, World world) : base(world)
        {
            Data = data;
        }

        private UfoData Data { get; set; }
    }
}
