using System;
using Trucker.Model.Dialogues;
using UnityEngine;

namespace Trucker.Model.Questing.Steps.Operations
{
    [CreateAssetMenu(menuName = "Quests/Operations/MiniDialogueStep", fileName = "MiniDialogueStep", order = 0)]
    public class MiniDialogueStep : Operation
    {
        public static event Action<IDialogue, Action> OnMiniDialogueRequest; 
        
        [SerializeField] private Dialogue dialogueToPlay;
        
        public override void Start()
        {
            Invoke(dialogueToPlay, onCompleted);
        }

        public void Invoke(IDialogue miniDialogue, Action onDialogueEndAction = null)
        {
            OnMiniDialogueRequest?.Invoke(miniDialogue, onDialogueEndAction);
        }
    }
}