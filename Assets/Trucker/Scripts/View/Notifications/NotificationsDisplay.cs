using System;
using System.Collections;
using System.Collections.Generic;
using Trucker.Model.Notifications;
using Trucker.Model.Questing.Quests;
using Trucker.Model.Questing.Steps.Goals;
using Trucker.Model.Rating;
using UnityEngine;
using UnityUtils.Variables;

namespace Trucker.View.Notifications
{
    public class NotificationsDisplay : MonoBehaviour
    {
        [SerializeField] private NotificationView[] notificationViews;
        [SerializeField] private FloatVariable delayBetweenNotifications;
        
        private readonly Queue<Notification> _notifications = new Queue<Notification>();
        private int _nextNotificationIndex;
        private Coroutine _notificationCoroutine;
        
        private void OnValidate() 
            => notificationViews = GetComponentsInChildren<NotificationView>(true);

        private void Awake()
        {
            DisableNotifications(); 
            Quest.OnQuestTaken += OnQuestTaken;
            Quest.OnQuestStop += OnQuestStop;
            Goal.OnStart += OnGoalStarted;
            Goal.OnCompletion += OnGoalCompleted;
            EmployeePerformanceRating.OnRatingChange += OnEmployeeRatingChange;
        }

        private void OnDestroy()
        {
            Quest.OnQuestTaken -= OnQuestTaken;
            Quest.OnQuestStop -= OnQuestStop;
            Goal.OnStart -= OnGoalStarted;
            Goal.OnCompletion -= OnGoalCompleted;
            EmployeePerformanceRating.OnRatingChange -= OnEmployeeRatingChange;
        }

        private void DisableNotifications()
        {
            foreach (var notification in notificationViews)
            {
                notification.gameObject.SetActive(false);
            }
        }

        private void OnEmployeeRatingChange(int ratingChange) 
            => Notify(new Notification {text = $"rating {(ratingChange > 0 ? "+" : "")}{ratingChange}"});

        private void OnQuestTaken(string title)
            => Notify(new Notification {text = $"{title} starts"});

        private void OnQuestStop(string title, QuestStatus questStatus)
        {
            string notificationText;
            
            switch (questStatus)
            {
                case QuestStatus.None: 
                case QuestStatus.Taken:
                    return;
                case QuestStatus.Completed:
                    notificationText = $"{title} completed";
                    break;
                case QuestStatus.Failed:
                    notificationText = $"{title} failed";
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(questStatus), questStatus, null);
            }
            
            Notify(new Notification {text = notificationText});
        }

        private void OnGoalStarted(string goalDescription)
            => Notify(new Notification {text = goalDescription});

        private void OnGoalCompleted(string goalDescription)
            => Notify(new Notification {text = goalDescription, strikethrough = true});

        private void Notify(Notification notification)
        {
            _notifications.Enqueue(notification);
            if (_notificationCoroutine == null)
            {
                _notificationCoroutine = StartCoroutine(DisplayNotificationCoroutine());
            }
        }

        private IEnumerator DisplayNotificationCoroutine()
        {
            while (_notifications.Count > 0)
            {
                DisplayNotification();
                yield return new WaitForSeconds(delayBetweenNotifications);
            }
            _notificationCoroutine = null;
        }

        private void DisplayNotification()
        {
            var notification = _notifications.Dequeue();
            notificationViews[_nextNotificationIndex].Display(notification);
            _nextNotificationIndex = (_nextNotificationIndex + 1) % notificationViews.Length;
        }
    }
}