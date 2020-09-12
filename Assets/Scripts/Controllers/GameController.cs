namespace AsteroidsClone
{
    public class GameController : Resident
    {
        public GameController(World world) : base(world)
        {
            var ship = new Ship(World);
            ship.Revive();
            World.Ship = ship;

            World.UpdateService.Add(ship);
        }
    }
}