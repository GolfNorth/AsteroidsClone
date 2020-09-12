namespace AsteroidsClone
{
    public sealed class GameController : Controller, ITickable
    {
        #region Constructor

        public GameController(World world) : base(world)
        {
            World.Ship = new Ship(World);
            World.UpdateService.Add(this);
            World.UpdateService.Add(World.Ship);
        }

        #endregion

        #region Methods

        public void Tick()
        {
            if (!World.Ship.IsActive && !World.Ship.IsDestroyed)
                World.Ship.Enable();

            if (World.Ship.IsDestroyed && World.InputService.Fire)
                RestartGame();
        }

        public override void RestartGame()
        {
            World.Ship.Revive();
            World.FireController.RestartGame();
            World.UfosController.RestartGame();
            World.AsteroidsController.RestartGame();
        }

        #endregion
    }
}