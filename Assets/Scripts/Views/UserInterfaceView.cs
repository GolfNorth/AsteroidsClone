using UnityEngine;
using UnityEngine.UI;

namespace AsteroidsClone
{
    public sealed class UserInterfaceView : MonoBehaviour
    {
        [SerializeField] private World world;
        [SerializeField] private Text score;
        [SerializeField] private Text laser;
        [SerializeField] private Text gameover;

        private void OnEnable()
        {
            score.enabled = true;
            laser.enabled = false;
            gameover.enabled = false;

            world.NotificationService.Notification += OnNotification;
        }

        private void OnDisable()
        {
            world.NotificationService.Notification -= OnNotification;
        }

        private void OnNotification(NotificationType notificationType, object obj)
        {
            switch (notificationType)
            {
                case NotificationType.UfoDestroyed:
                case NotificationType.AsteroidDestroyed:
                case NotificationType.ShipSpawned:
                    score.text = $"SCORE {world.ScoreController.Score}";
                    break;
            }
        }

        private void LateUpdate()
        {
            gameover.enabled = world.Ship.IsDestroyed;
            laser.enabled = world.FireController.IsLaserReady;
        }
    }
}