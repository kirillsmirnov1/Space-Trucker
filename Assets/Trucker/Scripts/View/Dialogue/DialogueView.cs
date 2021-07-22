using System.Linq;
using TMPro;
using Trucker.Model.NPC;
using Trucker.View.Util;
using UnityEngine;
using UnityEngine.UI;
using UnityUtils;

namespace Trucker.View.Dialogue
{
    public class DialogueView : ListView<string>
    {
        [SerializeField] private GameObject fade;
        [SerializeField] private TextMeshProUGUI npcNameText;
        [SerializeField] private Image npcPortrait;

        protected override void OnValidate()
        {
            base.OnValidate();
            this.CheckNullFields();
        }

        private void Awake() 
            => DialogueProvider.OnDialogueInitiated += ShowDialogue;

        private void OnDestroy() 
            => DialogueProvider.OnDialogueInitiated -= ShowDialogue;

        private void ShowDialogue(string npcName, Sprite npcPicture, IDialogue[] dialogues)
        {
            npcNameText.text = npcName;
            npcPortrait.sprite = npcPicture;
            fade.gameObject.SetActive(true);
            LayoutRebuilder.ForceRebuildLayoutImmediate(npcNameText.rectTransform);
            SetEntries(dialogues.Select(d => d.GetLines()[0]).ToList()); // IMPR this is the best way???
        }
    }
}