using System;
using System.Linq;
using UnityEngine;
using UnityUtils.Variables;

namespace Trucker.Model.Questing.Quests
{
    [CreateAssetMenu(fileName = "QuestLogEntries", menuName = "Quests/QuestLogEntries", order = 0)]
    public class QuestLogEntries : XArrayVariable<QuestLogEntryIdentificator>
    {
        public void QuestTaken(string title) 
            => UpdateQuestStatus(title, QuestStatus.Taken);

        public void QuestFinished(string title) 
            => UpdateQuestStatus(title, QuestStatus.Completed);

        public void QuestDropped(string title)
        {
            UpdateQuestStatus(title, QuestStatus.None);
        }

        private void UpdateQuestStatus(string title, QuestStatus status)
        {
            try
            {
                var val = value.data.First(entry => entry.title == title);
                val.status = status;
            }
            catch (InvalidOperationException)
            {
                var newEntry = new QuestLogEntryIdentificator {title = title, status = status}; 
                value.data = value.data.Concat(new[] {newEntry}).ToArray();
            }

            WriteSave();
        }
    }
}