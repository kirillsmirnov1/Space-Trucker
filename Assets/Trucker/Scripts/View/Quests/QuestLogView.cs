using UnityEngine;
using UnityUtils.Events;

namespace Trucker.View.Quests
{
    public class QuestLogView : MonoBehaviour
    {
        [SerializeField] private GameEvent openEvent;
        [SerializeField] private GameObject scroll;

        private void Awake() => openEvent.RegisterAction(Show);

        private void OnDestroy() => openEvent.UnregisterAction(Show);

        private void Show()
        {
            scroll.gameObject.SetActive(true);
            // TODO
        }
    }
}