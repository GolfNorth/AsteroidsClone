using System;
using UnityEngine;

namespace AsteroidsClone
{
    public abstract class Actor<TModel, TView, TData> : Resident
        where TModel : Model
        where TView : View<TModel>
        where TData : Data
    {
        private readonly TModel model;
        private readonly TData data;
        private TView view;

        public Actor(World world) : base(world)
        {
            GameObject gameObject;

            data = (TData) World.Data[typeof(TData)];
            model = (TModel) Activator.CreateInstance(typeof(TModel), new object[] { data });

            switch (World.ViewMode)
            {
                case ViewMode.Polygonal:
                    gameObject = GameObject.Instantiate(data.PolygonalPrefab);
                    break;
                case ViewMode.Sprite:
                default:
                    gameObject = GameObject.Instantiate(data.SpritePrefab);
                    break;
            }

            view = gameObject.AddComponent<TView>();
            view.Model = model;
        }

        public TModel Model => model;

        public TView View => view;
    }
}
