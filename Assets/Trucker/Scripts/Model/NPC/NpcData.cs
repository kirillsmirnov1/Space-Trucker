using UnityEngine;

namespace Trucker.Model.NPC
{
    public struct NpcData 
    {
        public string npcName;
        public Sprite npcPicture;
        public IDialogue[] dialogueOptions;

        public NpcData(string npcName, Sprite npcPicture, IDialogue[] dialogueOptions)
        {
            this.npcName = npcName;
            this.npcPicture = npcPicture;
            this.dialogueOptions = dialogueOptions;
        }
    }
}