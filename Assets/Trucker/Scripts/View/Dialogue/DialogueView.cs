using System.Collections.Generic;
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
        
        private IDialogue[] _dialogues;

        protected override void OnValidate()
        {
            base.OnValidate();
            this.CheckNullFields();
        }

        private void Awake()
        {
            DialogueProvider.OnDialogueInitiated += ShowDialogue;
            DialogueViewEntry.OnDialogueEntryClick += OnDialogueEntryClick;
        }

        private void OnDestroy()
        {
            DialogueProvider.OnDialogueInitiated -= ShowDialogue;
            DialogueViewEntry.OnDialogueEntryClick -= OnDialogueEntryClick;
        }

        private void ShowDialogue(string npcName, Sprite npcPicture, IDialogue[] dialogues)
        {
            _dialogues = dialogues;
            npcNameText.text = npcName;
            npcPortrait.sprite = npcPicture;
            fade.gameObject.SetActive(true);
            LayoutRebuilder.ForceRebuildLayoutImmediate(npcNameText.rectTransform);

            var lines = GetFirstLines(dialogues);
            lines.Add("Bye."); // TODO customize 
            
            SetEntries(lines);
        }

        private void HideDialogue()
        {
            fade.gameObject.SetActive(false);
        }

        private static List<string> GetFirstLines(IDialogue[] dialogues) // IMPR this is the best way???
        {
            return dialogues.Select(d => d.GetLines()[0]).ToList(); 
        }

        private void OnDialogueEntryClick(int dialogueIndex)
        {
            if (dialogueIndex >= _dialogues.Length)
            {
                HideDialogue();
            }
        }
    }
}