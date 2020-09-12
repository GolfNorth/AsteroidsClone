namespace AsteroidsClone
{
    public class ViewModeController : Resident, ITickable
    {
        private ViewMode _viewMode;

        public ViewModeController(World world) : base(world)
        {
            _viewMode = World.ViewMode;

            World.UpdateService.Add(this);
        }

        public void SwitchViewMode(ViewMode viewMode)
        {
            if (_viewMode == viewMode) return;

            World.ViewMode = _viewMode = viewMode;
            World.Ship.ViewMode = viewMode;
            World.Laser.ViewMode = viewMode;

            for (var i = 0; i < World.Asteroids.Count; i++)
                World.Asteroids[i].ViewMode = viewMode;

            for (var i = 0; i < World.Ufos.Count; i++)
                World.Ufos[i].ViewMode = viewMode;

            for (var i = 0; i < World.Bullets.Count; i++)
                World.Bullets[i].ViewMode = viewMode;
        }

        public void Tick()
        {
            if (_viewMode == World.ViewMode) return;

            SwitchViewMode(World.ViewMode);

            World.NotificationService.Notify(NotificationType.ViewModeChanged);
        }
    }
}