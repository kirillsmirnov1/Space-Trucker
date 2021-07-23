using System.Collections.Generic;
using TMPro;
using Trucker.Model.NPC;
using Trucker.View.Dialogue.State;
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

        internal IDialogue[] AllDialogues;
        private DialogueViewState _state;

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

        private void ShowDialogue(DialogueData dialogueData)
        {
            SetViewData(dialogueData);
            SetState(new DialogueOptions(this));
            ShowView();
        }

        public void SetState(DialogueViewState newState)
        {
            _state?.Stop();
            _state = newState;
            _state.Start();
        }

        private void SetViewData(DialogueData dialogueData)  
        {
            AllDialogues = dialogueData.dialogueOptions;
            npcNameText.text = dialogueData.npcName;
            npcPortrait.sprite = dialogueData.npcPicture;
        }

        private void ShowView()
        {
            fade.gameObject.SetActive(true);
            LayoutRebuilder.ForceRebuildLayoutImmediate(npcNameText.rectTransform);
        }

        public void HideDialogue()
        {
            fade.gameObject.SetActive(false);
        }

        private void OnDialogueEntryClick(int dialogueIndex) 
            => _state.OnDialogueEntryClick(dialogueIndex);

        public void SetLines(List<string> lines) 
            => SetEntries(lines);
    }
}