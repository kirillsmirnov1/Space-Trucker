using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Trucker.Model.NPC;
using Trucker.View.Dialogue.State;
using Trucker.View.Util;
using UnityEngine;
using UnityEngine.UI;
using UnityUtils;
using UnityUtils.Extensions;

namespace Trucker.View.Dialogue
{
    public class DialogueView : ListView<string>
    {
        public static event Action OnOpen;
        public static event Action OnClose;
        
        [Header("Dialogue View")]
        [SerializeField] private GameObject fade;
        [SerializeField] private TextMeshProUGUI npcNameText;
        [SerializeField] public CharacterPortrait characterPortrait;
        [SerializeField] private CharactersData charactersData;
        [SerializeField] private RectTransform namePlank;
        
        private IDialogue[] _allDialogues;
        private DialogueViewState _state;
        public CharacterName defaultCharacter;
        private CharacterName _lastCharacter = CharacterName.None;

        private IDialogue[] AvailableDialogues 
            => _allDialogues.Where(x => x.AvailableAsDialogueOption()).ToArray();

        protected override void OnValidate()
        {
            base.OnValidate();
            this.CheckNullFields();
        }

        private void Awake()
        {
            NpcDialogueProvider.OnDialogueInitiated += ShowDialogue;
            DialogueViewEntry.OnDialogueEntryClick += OnDialogueEntryClick;
        }

        private void OnDestroy()
        {
            NpcDialogueProvider.OnDialogueInitiated -= ShowDialogue;
            DialogueViewEntry.OnDialogueEntryClick -= OnDialogueEntryClick;
        }

        private void ShowDialogue(NpcData npcData)
        {
            SetViewData(npcData);
            SetStateDialogueOptions();
            ShowView();
        }

        private void SetViewData(NpcData npcData)  
        {
            _allDialogues = npcData.dialogueOptions;
            defaultCharacter = npcData.characterName;
        }

        private void SetState(DialogueViewState newState)
        {
            _state?.Stop();
            _state = newState;
            _state.Start();
        }

        public void SetStateDialogueOptions() 
            => SetState(new DialogueOptions(this, AvailableDialogues));

        public void SetStateDialogueInProgress(IDialogue dialogue) 
            => SetState(new DialogueInProgress(this, dialogue));

        private void ShowView()
        {
            fade.gameObject.SetActive(true);
            LayoutRebuilder.ForceRebuildLayoutImmediate(npcNameText.rectTransform);
            OnOpen?.Invoke();
        }

        public void HideDialogue()
        {
            fade.gameObject.SetActive(false);
            OnClose?.Invoke();
        }

        private void OnDialogueEntryClick(int dialogueIndex) 
            => _state.OnDialogueEntryClick(dialogueIndex);

        public void SetLines(List<string> lines) 
            => SetEntries(lines);

        public void SetCharacter(CharacterName character)
        {
            if(character == _lastCharacter) return;
            _lastCharacter = character;
            var characterData = charactersData.GetCharacterData(character);
            npcNameText.text = characterData.nameText;
            characterPortrait.SetPortrait(characterData.portrait);
            this.DelayAction(0f, () => LayoutRebuilder.ForceRebuildLayoutImmediate(namePlank));
        }
    }
}