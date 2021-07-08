using TMPro;
using Trucker.Model.Questing.Quests;
using UnityEngine;
using UnityUtils;

namespace Trucker.View.Quests
{
    public class QuestView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI titleText;
        [SerializeField] private TextMeshProUGUI goalsText;
        
        private void OnValidate() => this.CheckNullFields();

        public void Fill(Quest quest)
        {
            gameObject.SetActive(true);
            titleText.text = quest.title;
            // TODO fill quest description text 
            goalsText.text = quest.GoalsText;
        }
        
        // TODO change QuestView size 
    }
}