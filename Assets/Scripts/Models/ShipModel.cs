using TMPro;
using UnityEngine;

namespace AsteroidsClone
{
    public sealed class ShipModel : Model
    {
        public ShipModel(ShipData data, World world) : base(world)
        {
            Data = data;
            Shape = Data.Shape;
        }

        private ShipData Data { get; set; }

        public PolygonShape Shape { get; set; }

        public void Move(float translation, float rotation)
        {
            if (rotation != 0)
            {
                Angle -= rotation * Data.AngularSpeed * Time.fixedDeltaTime;
            }

            if (translation > 0)
            {
                Velocity += Direction * Data.Speed * Time.fixedDeltaTime;

                if (Velocity.magnitude > Data.Speed)
                    Velocity = Velocity.normalized * Data.Speed;
            }
            else
            {
                Velocity = Vector2.Distance(Velocity, Vector2.zero) > Data.StopSpeed
                    ? Vector2.Lerp(Velocity, Vector2.zero, (1 - Data.Inertia) * Time.fixedDeltaTime)
                    : Vector2.zero;
            }

            var position = Position + Velocity * Time.fixedDeltaTime;

            World.BoundsService.WrapCoordinates(position, ref position);

            Position = position;
        }
    }
}
