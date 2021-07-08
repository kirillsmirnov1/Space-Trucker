using System;

namespace Trucker.Model.Questing.Quests
{
    [Serializable]
    public struct QuestLogEntryIdentificator
    {
        public string title;
        public QuestStatus status;
    }
}