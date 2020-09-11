using System;

namespace AsteroidsClone
{
    public sealed class NotificationService
    {
        public Action<NotificationType, object> Notification;

        public void Notify(NotificationType notificationType, object obj = null)
        {
            Notification?.Invoke(notificationType, obj);
        }
    }
}