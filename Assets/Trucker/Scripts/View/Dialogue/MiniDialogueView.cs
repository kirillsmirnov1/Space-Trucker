using System;
using TMPro;
using Trucker.Model.Dialogues;
using Trucker.Model.NPC;
using Trucker.Model.Questing.Steps.Operations;
using UnityEngine;
using UnityUtils.Variables;

namespace Trucker.View.Dialogue
{
    public class MiniDialogueView : MonoBehaviour
    {
        [SerializeField] private GameObject dialogueWrap;
        [SerializeField] private CharacterPortrait characterPortrait;
        [SerializeField] private TextMeshProUGUI dialogueText;
        [SerializeField] private CharactersData charactersData;
        [SerializeField] private FloatVariable secondsPerLine;
        
        private DialogueLine[] _lines;
        private Action _finishCallback;
        private int _lineIndex;
        private float _lineTimeLeft;
        private Action _onUpdate;

        private void Awake() 
            => MiniDialogueStep.OnMiniDialogueRequest += ShowMiniDialogue;

        private void OnDestroy() 
            => MiniDialogueStep.OnMiniDialogueRequest -= ShowMiniDialogue;

        private void Update() 
            => _onUpdate?.Invoke();

        private void ShowMiniDialogue(IDialogue dialogue, Action finishCallback)
        {
            _lines = dialogue.Lines;
            _finishCallback = finishCallback;
            _lineIndex = -1;

            IterateLines();
            dialogueWrap.SetActive(true);
        }

        public void IterateLines()
        {
            _onUpdate = null;
            _lineIndex++;
            if (_lineIndex < _lines.Length)
            {
                SetNextLine();
                ResetLineTimer();
            }
            else
            {
                dialogueWrap.SetActive(false);
                _finishCallback?.Invoke();
            }
        }

        private void SetNextLine()
        {
            characterPortrait.SetPortrait(charactersData.GetCharacterData(_lines[_lineIndex].character).portrait);
            dialogueText.text = _lines[_lineIndex].line;
        }

        private void ResetLineTimer()
        {
            _lineTimeLeft = secondsPerLine;
            _onUpdate = TickDialogueTime;
        }

        private void TickDialogueTime()
        {
            _lineTimeLeft -= Time.deltaTime;
            
            if (_lineTimeLeft <= 0f)
            {
                IterateLines();
            }
        }
    }
}