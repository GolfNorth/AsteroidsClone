namespace AsteroidsClone
{
    public class GameController : Resident
    {
        public GameController(World world) : base(world)
        {
            var ship = new Ship(World);
            ship.Enable();
            World.Ship = ship;

            World.UpdateService.Add(ship);
        }
    }
}