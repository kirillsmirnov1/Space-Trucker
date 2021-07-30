using Trucker.Model.Entities;
using Trucker.Model.Questing.Consequences;
using Trucker.Model.Questing.Goals;
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
            DisableNotifications(); 
            Quest.OnQuestTaken += OnQuestTaken;
            Quest.OnQuestFinished += OnQuestFinished;
            Goal.OnStart += OnGoalStarted;
            Goal.OnCompletion += OnGoalCompleted;
            DestroyCatchedObjects.OnObjectsDestroyed += OnCatchedObjectsDestroyed;
        }

        private void OnDestroy()
        {
            Quest.OnQuestTaken -= OnQuestTaken;
            Quest.OnQuestFinished -= OnQuestFinished;
            Goal.OnStart -= OnGoalStarted;
            Goal.OnCompletion -= OnGoalCompleted;
            DestroyCatchedObjects.OnObjectsDestroyed -= OnCatchedObjectsDestroyed;
        }

        private void DisableNotifications()
        {
            foreach (var notification in notifications)
            {
                notification.gameObject.SetActive(false);
            }
        }

        private void OnQuestTaken(string title) 
            => Notify($"{title} starts", false);

        private void OnQuestFinished(string title) 
            => Notify($"{title} finished", false);

        private void OnGoalStarted(string goalDescription) 
            => Notify(goalDescription, false);

        private void OnGoalCompleted(string goalDescription) 
            => Notify(goalDescription, true);

        private void OnCatchedObjectsDestroyed(EntityType type, int count)
        {
            var multiple = count > 1;
            var str = $"{(multiple ? $"{count} " : "")} {type.ToString()}{(multiple ? "s" : "")} detached";
            Notify(str, false);
        }

        private void Notify(string str, bool strikethrough)
        {
            notifications[_nextNotificationIndex].Display(str, strikethrough);
            _nextNotificationIndex = (_nextNotificationIndex + 1) % notifications.Length;
        }
    }
}