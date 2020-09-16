using System.Collections.Generic;
using AsteroidsCore;
using UnityEngine;

namespace AsteroidsView
{
    public sealed class ViewModeManager : Manager
    {
        #region Fields

        private ViewMode _prevViewMode;
        private Dictionary<ViewMode, Dictionary<IActor, IView>> _views;

        #endregion

        #region Methods

        private void Awake()
        {
            _views = new Dictionary<ViewMode, Dictionary<IActor, IView>>
            {
                {ViewMode.Polygonal, new Dictionary<IActor, IView>()},
                {ViewMode.Sprite, new Dictionary<IActor, IView>()}
            };
        }

        private void Update()
        {
            if (!Input.GetKeyDown(KeyCode.Tab)) return;

            _prevViewMode = Context.ViewMode;
            Context.ViewMode = Context.ViewMode == ViewMode.Polygonal
                ? ViewMode.Sprite
                : ViewMode.Polygonal;

            SwitchViewMode(Context.World.Ship);
            SwitchViewMode(Context.World.Laser);

            for (var i = 0; i < Context.World.Asteroids.Count; i++)
                SwitchViewMode(Context.World.Asteroids[i]);

            for (var i = 0; i < Context.World.Ufos.Count; i++)
                SwitchViewMode(Context.World.Ufos[i]);

            for (var i = 0; i < Context.World.Bullets.Count; i++)
                SwitchViewMode(Context.World.Bullets[i]);
        }

        private void SwitchViewMode<TModel>(Actor<TModel> actor) where TModel : Model
        {
            if (!_views[_prevViewMode].ContainsKey(actor)) _views[_prevViewMode].Add(actor, actor.View);

            var newView = _views[Context.ViewMode].ContainsKey(actor)
                ? _views[Context.ViewMode][actor]
                : Context.ViewFactory.CreateView(actor.Model);

            newView.IsActive = true;
            actor.View.IsActive = false;
            actor.View = (IView<TModel>) newView;
        }

        #endregion
    }
}