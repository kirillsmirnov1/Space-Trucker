using System;
using System.Linq;
using UnityEngine;
using UnityUtils;

namespace Trucker.Model.NPC
{
    public class NpcDialogueProvider : MonoBehaviour 
    {
        public static event Action<NpcData> OnDialogueInitiated;

        [SerializeField] private string npcName;
        [SerializeField] private Sprite npcPicture;
        [SerializeField] private Dialogue[] dialogueOptions; // Unity still doesnt serialize interfaces, so here we go 

        private void OnValidate() 
            => this.CheckNullFields();

        private IDialogue[] GetDialogues() 
            => dialogueOptions.Select(x => (IDialogue) x).ToArray();

        public void InitiateDialogue()
            => OnDialogueInitiated?.Invoke(DialogueData());

        private NpcData DialogueData() 
            => new NpcData(npcName, npcPicture, GetDialogues());
    }
}