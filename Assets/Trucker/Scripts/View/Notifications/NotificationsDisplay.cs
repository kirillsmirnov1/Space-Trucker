using Trucker.Model.Entities;
using Trucker.Model.Notifications;
using Trucker.Model.Questing.Quests;
using Trucker.Model.Questing.Steps.Goals;
using Trucker.Model.Questing.Steps.Operations;
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
            => Notify(new Notification {text = $"{title} starts"});

        private void OnQuestFinished(string title)
            => Notify(new Notification {text = $"{title} finished"});

        private void OnGoalStarted(string goalDescription)
            => Notify(new Notification {text = goalDescription});

        private void OnGoalCompleted(string goalDescription)
            => Notify(new Notification {text = goalDescription, strikethrough = true});

        private void OnCatchedObjectsDestroyed(EntityType type, int count)
        {
            var multiple = count > 1;
            var str = $"{(multiple ? $"{count} " : "")} {type.ToString()}{(multiple ? "s" : "")} detached";
            Notify(new Notification {text = str});
        }

        private void Notify(Notification notification)
        {
            notifications[_nextNotificationIndex].Display(notification);
            _nextNotificationIndex = (_nextNotificationIndex + 1) % notifications.Length;
        }
    }
}