using UnityEngine;

namespace AsteroidsClone
{
    public sealed class AsteroidModel : Model
    {
        private AsteroidSize _size;

        public AsteroidModel(AsteroidData data, World world) : base(world)
        {
            Data = data;
        }

        public AsteroidSize Size {
            get => _size;
            set
            {
                if (_size == value) return;

                _size = value;
                Radius = Data.Settings[value].Radius;
                Shape = new CircleShape { Radius = Radius };
            }
        }

        private AsteroidData Data { get; set; }

        public CircleShape Shape { get; set; }

        public float Radius { get; set; }

        public float Speed { get; set; }

        public void RandomizeAngleAndSpeed()
        {
            Angle = Random.Range(0, 360);
            Speed = Random.Range(Data.Settings[Size].MinSpeed, Data.Settings[Size].MaxSpeed);

            Velocity = Direction * Speed;
        }

        public void RandomizePosition()
        {
            Position = World.BoundsService.RandomizePosition(Radius);
        }

        public void RandomizeSize()
        {
            Size = (AsteroidSize)Random.Range(1, System.Enum.GetValues(typeof(AsteroidSize)).Length);
        }

        public void Move()
        {
            var position = Position + Velocity * Time.fixedDeltaTime;

            World.BoundsService.WrapCoordinates(position, ref position, Data.Settings[Size].Radius);

            Position = position;
        }
    }
}
