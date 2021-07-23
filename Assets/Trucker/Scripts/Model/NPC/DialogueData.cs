using UnityEngine;

namespace Trucker.Model.NPC
{
    public struct DialogueData
    {
        public string npcName;
        public Sprite npcPicture;
        public IDialogue[] dialogueOptions;

        public DialogueData(string npcName, Sprite npcPicture, IDialogue[] dialogueOptions)
        {
            this.npcName = npcName;
            this.npcPicture = npcPicture;
            this.dialogueOptions = dialogueOptions;
        }
    }
}