using UnityEngine;

namespace AsteroidsClone
{
    public sealed class Asteroid : Actor<AsteroidModel, AsteroidView, AsteroidData>, IHit, IPoolable, IFixedTickable
    {
        public Asteroid(World world) : base(world)
        {
        }

        public bool IsActive { get; set; }
        public CircleShape Shape => Model.Shape;

        public void Disable()
        {
            View.gameObject.SetActive(false);
        }

        public void Enable()
        {
            Model.Randomize();
            View.gameObject.SetActive(true);
        }

        public void FixedTick()
        {
            if (!IsActive) return;

            Model.Move();
        }

        public void Hit()
        {
            switch (Model.Size)
            {
                case AsteroidSize.Large:
                    break;
                case AsteroidSize.Middle:
                    break;
                case AsteroidSize.Small:
                    break;
            }
        }
    }
}
