using AsteroidsCore;
using UnityEngine;
using UnityEngine.UI;

namespace AsteroidsView
{
    public sealed class UserInterfaceManager : Manager
    {
        #region Fields

        private Text _score;
        private Text _laser;
        private Text _gameover;

        #endregion

        #region Methods

        protected override void OnContextChanged()
        {
            _score = GameObject.Find("ScoreText").GetComponent<Text>();
            _laser = GameObject.Find("LaserText").GetComponent<Text>();
            _gameover = GameObject.Find("GameOverText").GetComponent<Text>();

            _score.enabled = true;
            _laser.enabled = false;
            _gameover.enabled = false;

            Context.World.NotificationService.Notification += OnNotification;
        }

        private void LateUpdate()
        {
            if (Context is null) return;

            _gameover.enabled = Context.World.Ship.IsDestroyed;
            _laser.enabled = Context.World.FireController.IsLaserReady;
        }

        private void OnDisable()
        {
            if (Context is null) return;

            Context.World.NotificationService.Notification -= OnNotification;
        }

        private void OnNotification(NotificationType notificationType, object obj)
        {
            switch (notificationType)
            {
                case NotificationType.UfoDestroyed:
                case NotificationType.AsteroidDestroyed:
                case NotificationType.ShipSpawned:
                    _score.text = $"SCORE {Context.World.ScoreController.Score}";
                    break;
            }
        }

        #endregion
    }
}