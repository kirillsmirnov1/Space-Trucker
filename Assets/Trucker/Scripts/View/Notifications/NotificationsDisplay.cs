using Trucker.Model.Questing.Quests;
using UnityEngine;

namespace Trucker.View.Notifications
{
    public class NotificationsDisplay : MonoBehaviour
    {
        [SerializeField] private NotificationView[] notifications;

        private int _nextNotificationIndex;
        
        private void OnValidate() 
            => notifications = GetComponentsInChildren<NotificationView>(true);

        private void Awake()
        {
            Quest.OnQuestTaken += OnQuestTaken;
            Quest.OnQuestFinished += OnQuestFinished;
            // TODO goals 
            // TODO object detach 
        }

        private void OnDestroy()
        {
            Quest.OnQuestTaken -= OnQuestTaken;
            Quest.OnQuestFinished -= OnQuestFinished;
        }

        private void OnQuestTaken(string title) 
            => Notify($"{title} starts", false);

        private void OnQuestFinished(string title) 
            => Notify($"{title} finished", false);

        private void Notify(string str, bool strikethrough)
        {
            notifications[_nextNotificationIndex].Display(str, strikethrough);
            _nextNotificationIndex = (_nextNotificationIndex + 1) % notifications.Length;
        }
    }
}