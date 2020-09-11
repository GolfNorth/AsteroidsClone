using UnityEngine;

namespace AsteroidsClone
{
    public sealed class BulletModel : Model
    {
        public BulletModel(BulletData data, World world) : base(world)
        {
            Data = data;
        }

        private BulletData Data { get; set; }
    }
}
