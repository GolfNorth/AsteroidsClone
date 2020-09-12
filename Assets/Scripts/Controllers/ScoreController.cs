namespace AsteroidsClone
{
    public sealed class ScoreController : Controller
    {
        #region Constructor

        public ScoreController(World world) : base(world)
        {
            world.NotificationService.Notification += OnNotification;
        }

        #endregion

        #region Properties

        public int Score { get; private set; }

        #endregion

        #region Methods

        private void OnNotification(NotificationType notificationType, object obj)
        {
            switch (notificationType)
            {
                case NotificationType.UfoDestroyed:
                case NotificationType.AsteroidDestroyed:
                    Score++;
                    break;
                case NotificationType.ShipSpawned:
                    Score = 0;
                    break;
            }
        }

        #endregion
    }
}