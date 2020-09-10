using System;

namespace AsteroidsClone
{
    public class NotificationService
    {
        public Action<NotificationType> Notification;

        public void Notify(NotificationType notificationType)
        {
            Notification?.Invoke(notificationType);
        }
    }
}