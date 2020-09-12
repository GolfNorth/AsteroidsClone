namespace AsteroidsClone
{
    public class GameController : Resident, ITickable
    {
        public GameController(World world) : base(world)
        {
            World.Ship = new Ship(World);
            World.UpdateService.Add(this);
            World.UpdateService.Add(World.Ship);
        }

        public void Tick()
        {
            if (!World.Ship.IsActive && !World.Ship.IsDestroyed)
                World.Ship.Enable();

            if (World.Ship.IsDestroyed && World.InputService.Fire)
                RestartGame();

        }

        public void RestartGame()
        {
            World.Ship.Revive();
            World.FireController.RestartGame();
            World.UfosController.RestartGame();
            World.AsteroidsController.RestartGame();
        }
    }
}