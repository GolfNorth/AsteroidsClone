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

        public void Randomize(AsteroidSize size = AsteroidSize.None, float minAngle = 0, float maxAngle = 360)
        {
            if (size == AsteroidSize.None)
                size = (AsteroidSize) Random.Range(1, System.Enum.GetValues(typeof(AsteroidSize)).Length);

            Angle = Random.Range(minAngle, maxAngle);
            Speed = Random.Range(Data.Settings[size].MinSpeed, Data.Settings[size].MaxSpeed);

            Velocity = Direction * Speed;
        }

        public void Move()
        {
            Position += Velocity * Time.fixedDeltaTime;
        }
    }
}
