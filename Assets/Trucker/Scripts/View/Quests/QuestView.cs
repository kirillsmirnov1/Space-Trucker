using TMPro;
using Trucker.Model.Questing.Quests;
using UnityEngine;
using UnityUtils;
using UnityUtils.View;

namespace Trucker.View.Quests
{
    public class QuestView : ListViewEntry<Quest>
    {
        [SerializeField] private TextMeshProUGUI titleText;
        [SerializeField] private TextMeshProUGUI descriptionText;
        [SerializeField] private TextMeshProUGUI goalsText;
        
        private void OnValidate() => this.CheckNullFields();

        public override void Fill(Quest quest)
        {
            base.Fill(quest);
            titleText.text = quest.title;
            descriptionText.text = quest.description;
            goalsText.text = quest.GoalsText;
        }
    }
}