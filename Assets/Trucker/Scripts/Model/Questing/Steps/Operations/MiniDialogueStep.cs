using System;
using Trucker.Model.NPC;
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
            OnMiniDialogueRequest?.Invoke(dialogueToPlay, onCompleted);
        }
    }
}