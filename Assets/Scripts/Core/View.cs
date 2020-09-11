using UnityEngine;

namespace AsteroidsClone
{
    public abstract class View<T> : MonoBehaviour, IFixedTickable where T : Model
    {
        private T model;

        public T Model { protected get => model; set => model = value; }

        public virtual void FixedTick()
        {
            transform.position = model.Position;
            transform.rotation = Quaternion.Euler(0, 0, model.Angle);
        }
    }
}
