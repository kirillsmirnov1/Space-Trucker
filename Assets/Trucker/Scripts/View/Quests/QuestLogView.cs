using Trucker.Model.Questing.Quests;
using Trucker.View.Util;
using UnityEngine;
using UnityUtils;
using UnityUtils.Events;

namespace Trucker.View.Quests
{
    public class QuestLogView : ListView<Quest>
    {
        [SerializeField] private GameEvent openEvent;
        [SerializeField] private GameObject scroll;
        [SerializeField] private GameObject fade;
        [SerializeField] private QuestLog questLog;

        protected override void OnValidate()
        {
            base.OnValidate();
            this.CheckNullFields();
        }

        private void Awake() => openEvent.RegisterAction(Show);
        private void OnDestroy() => openEvent.UnregisterAction(Show);

        private void Show()
        {
            SetEntries(questLog.Taken);
            scroll.SetActive(true);
            fade.SetActive(true); // TODO create some better animation 
        }
    }
}