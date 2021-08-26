using System;
using TMPro;
using Trucker.Model.NPC;
using Trucker.Model.Questing.Steps.Operations;
using UnityEngine;

namespace Trucker.View.Dialogue
{
    public class MiniDialogueView : MonoBehaviour
    {
        [SerializeField] private CharacterPortrait characterPortrait;
        [SerializeField] private TextMeshProUGUI dialogueText;
        [SerializeField] private CharactersData charactersData;
        
        private void Awake() 
            => MiniDialogueStep.OnMiniDialogueRequest += ShowMiniDialogue;

        private void OnDestroy() 
            => MiniDialogueStep.OnMiniDialogueRequest -= ShowMiniDialogue;

        private void ShowMiniDialogue(IDialogue dialogue, Action finishCallback)
        {
            // TODO iterate over lines 
            // TODO invoke finish callback 
        }
    }
}