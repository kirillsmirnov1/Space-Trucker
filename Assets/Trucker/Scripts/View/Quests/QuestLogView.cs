using System.Collections.Generic;
using Trucker.Model.Questing.Quests;
using UnityEngine;
using UnityUtils;
using UnityUtils.Events;

namespace Trucker.View.Quests
{
    public class QuestLogView : MonoBehaviour
    {
        [SerializeField] private GameEvent openEvent;
        [SerializeField] private GameObject scroll;
        [SerializeField] private Transform scrollContent; 
        [SerializeField] private List<QuestView> questViews;
        [SerializeField] private GameObject questViewPrefab;
        [SerializeField] private QuestLog questLog;

        private void OnValidate()
        {
            questViews = new List<QuestView>(GetComponentsInChildren<QuestView>(true));
            this.CheckNullFields();
        }

        private void Awake() => openEvent.RegisterAction(Show);
        private void OnDestroy() => openEvent.UnregisterAction(Show);

        private void Show()
        {
            SetQuestViews();
            scroll.gameObject.SetActive(true);
        }

        private void SetQuestViews()
        {
            var takenQuests = questLog.Taken;
            CheckConsistency(takenQuests);
            FillData(takenQuests);
        }

        private void CheckConsistency(List<Quest> takenQuests)
        {
            var viewsToTakenDiff = questViews.Count - takenQuests.Count;

            if (viewsToTakenDiff < 0)
            {
                SpawnViews(viewsToTakenDiff);
            }
            else if (viewsToTakenDiff > 0)
            {
                DisableViews(viewsToTakenDiff);
            }
        }

        private void SpawnViews(int viewsToSpawn)
        {
            for (int i = 0; i < viewsToSpawn; i++)
            {
                questViews.Add(Instantiate(questViewPrefab, scrollContent).GetComponent<QuestView>());
            }
        }

        private void DisableViews(int viewsToDisable)
        {
            for (int i = 0; i < viewsToDisable; i++)
            {
                questViews[questViews.Count - 1 - i].gameObject.SetActive(false);
            }
        }

        private void FillData(List<Quest> takenQuests)
        {
            for (int i = 0; i < takenQuests.Count; i++)
            {
                questViews[i].Fill(takenQuests[i]);
            }
        }
    }
}