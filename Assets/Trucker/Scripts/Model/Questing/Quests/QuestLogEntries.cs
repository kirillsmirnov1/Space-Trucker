using System;
using System.Linq;
using UnityEngine;
using UnityUtils.Variables;

namespace Trucker.Model.Questing.Quests
{
    [CreateAssetMenu(fileName = "QuestLogEntries", menuName = "Quests/QuestLogEntries", order = 0)]
    public class QuestLogEntries : XArrayVariable<QuestLogEntryIdentificator>
    {
        private static QuestLogEntries _instance;

        public override void Init()
        {
            base.Init();
            _instance = this;
        }

        public void QuestTaken(string title) 
            => UpdateQuestStatus(title, QuestStatus.Taken);

        public void QuestStopped(string title, QuestStatus questStatus) 
            => UpdateQuestStatus(title, questStatus);

        private void UpdateQuestStatus(string title, QuestStatus status)
        {
            try
            {
                var val = GetIdentificatorByTitle(title);
                val.status = status;
            }
            catch (InvalidOperationException)
            {
                var newEntry = new QuestLogEntryIdentificator {title = title, status = status}; 
                value.data = value.data.Concat(new[] {newEntry}).ToArray();
            }

            WriteSave();
        }

        public static bool NeverBeenStarted(string title) 
            => _instance.NeverBeenStartedImpl(title);

        private bool NeverBeenStartedImpl(string title)
        {
            try
            {
                var val = GetIdentificatorByTitle(title);
                return val.status == QuestStatus.None;
            }
            catch (InvalidOperationException)
            {
                return true;
            }
        }

        public static bool NotFailed(string title) 
            => _instance.NotFailedImpl(title);

        private bool NotFailedImpl(string title)
        {
            try
            {
                var val = GetIdentificatorByTitle(title);
                return val.status != QuestStatus.Failed;
            }
            catch (InvalidOperationException)
            {
                return true;
            }
        }

        private QuestLogEntryIdentificator GetIdentificatorByTitle(string title) 
            => value.data.First(entry => entry.title == title);

        public void Clear() => Value = new QuestLogEntryIdentificator[0];
    }
}