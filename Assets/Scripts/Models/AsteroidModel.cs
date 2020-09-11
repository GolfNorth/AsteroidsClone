using UnityEngine;

namespace AsteroidsClone
{
    public sealed class AsteroidModel : Model
    {
        private readonly AsteroidData Data;
        private AsteroidSize _size;

        public AsteroidModel(AsteroidData data)
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
        public CircleShape Shape { get; set; }
        public float Speed { get; set; }
        public float Radius { get; set; }

        public void Randomize(AsteroidSize size = AsteroidSize.None, float minAngle = 0, float maxAngle = 360)
        {
            Debug.Log(1);

            if (size == AsteroidSize.None)
            {
                size = (AsteroidSize) Random.Range(1, System.Enum.GetValues(typeof(AsteroidSize)).Length);
            }

            Angle = Random.Range(minAngle, maxAngle);
            Speed = Random.Range(Data.Settings[size].MinSpeed, Data.Settings[size].MaxSpeed);

            UpdateVelocity();
        }

        public void UpdateVelocity()
        {
            var velocity = new Vector2
            {
                x = Mathf.Cos(Angle * Mathf.Deg2Rad),
                y = Mathf.Sin(Angle * Mathf.Deg2Rad)
            };

            Velocity = velocity.normalized * Speed;
        }

        public void Move()
        {
            var position = Vector2.MoveTowards(Position, Position + Velocity * 10, Time.fixedDeltaTime * Speed);

            Position = position;
        }
    }
}
