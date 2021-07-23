using System;
using System.Linq;
using UnityEngine;
using UnityUtils;

namespace Trucker.Model.NPC
{
    public class DialogueProvider : MonoBehaviour
    {
        public static event Action<DialogueData> OnDialogueInitiated;

        [SerializeField] private string npcName;
        [SerializeField] private Sprite npcPicture;
        [SerializeField] private Dialogue[] dialogueOptions; // Unity still doesnt serialize interfaces, so here we go 

        private void OnValidate() 
            => this.CheckNullFields();

        private IDialogue[] GetDialogues() 
            => dialogueOptions.Select(x => (IDialogue) x).ToArray();

        public void InitiateDialogue()
            => OnDialogueInitiated?.Invoke(DialogueData());

        private DialogueData DialogueData() 
            => new DialogueData(npcName, npcPicture, GetDialogues());
    }
}