using UnityEngine;

namespace AsteroidsClone
{
    public sealed class AsteroidModel : Model
    {
        private readonly AsteroidData _data;
        private AsteroidSize _size;
        private float _speed;
        private float _direction;

        public AsteroidModel(AsteroidData data, AsteroidSize size)
        {
            _data = data;

            SwitchSize(size);
        }

        public AsteroidSize Size { get => _size; set => _size = value; }
        public float Speed { get => _speed; set => _speed = value; }
        public float Direction { get => _direction; set => _direction = value; }

        public void SwitchSize(AsteroidSize newSize)
        {
            Size = newSize;
            Direction = Random.Range(0, 360);
            Speed = Random.Range(_data.Settings[newSize].MinSpeed, _data.Settings[newSize].MaxSpeed);
        }
    }
}
