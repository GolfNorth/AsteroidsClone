using System;

namespace AsteroidsClone
{
    public class NotificationService
    {
        private Action<NotificationType> Notification;

        public void Notify(NotificationType notificationType)
        {
            Notification?.Invoke(notificationType);
        }
    }
}