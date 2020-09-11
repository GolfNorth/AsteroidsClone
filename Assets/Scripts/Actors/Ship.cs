using UnityEngine;

namespace AsteroidsClone
{
    public sealed class Ship : Actor<ShipModel, ShipView, ShipData>, ITickable, IFixedTickable
    {
        private readonly InputService inputService;
        private readonly FireController fireController;

        public Ship(World world) : base(world)
        {
            inputService = World.InputService;
            fireController = World.FireController;
        }

        public void Tick()
        {
            if (inputService.Fire) Fire();

            if (inputService.AltFire) AltFire();
        }

        public void FixedTick()
        {
            Model.Move(inputService.Translation, inputService.Rotation);
        }

        public void Fire()
        {
            fireController.Fire(Model.Position, Model.Angle);
        }

        public void AltFire()
        {
            fireController.Fire(Model.Position, Model.Angle);
        }
    }
}
