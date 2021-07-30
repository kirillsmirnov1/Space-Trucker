using UnityEngine;

namespace Trucker.View.Notifications
{
    public class NotificationsDisplay : MonoBehaviour
    {
        [SerializeField] private NotificationView[] notifications;

        private void OnValidate()
        {
            notifications = GetComponentsInChildren<NotificationView>(true);
        }
    }
}