using System;
using System.Linq;
using UnityEngine;

namespace Trucker.Model.NPC
{
    public class DialogueProvider : MonoBehaviour
    {
        public static event Action<IDialogue[]> OnDialogueInitiated; 
        
        // TODO sprite and name
        [SerializeField] private ScriptableObject[] dialogueOptions; // Unity still doesnt serialize interfaces, so here we go 

        private IDialogue[] GetDialogues() 
            => dialogueOptions.Select(x => (IDialogue) x).ToArray();

        public void InitiateDialogue()
            => OnDialogueInitiated?.Invoke(GetDialogues());
    }
}