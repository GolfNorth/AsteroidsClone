namespace AsteroidsClone
{
    public class ScoreController : Resident
    {
        private int _score;

        public ScoreController(World world) : base(world)
        {
            world.NotificationService.Notification += OnNotification;
        }

        private void OnNotification(NotificationType notificationType, object obj)
        {
            switch (notificationType)
            {
                case NotificationType.UfoDestroyed:
                case NotificationType.AsteroidDestroyed:
                    _score++;
                    break;
                case NotificationType.ShipSpawned:
                    _score = 0;
                    break;
            }
        }

        public int Score => _score;
    }
}