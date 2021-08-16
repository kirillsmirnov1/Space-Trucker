using UnityEngine;

namespace Trucker.Model.NPC
{
    public struct NpcData 
    {
        public CharacterName characterName;
        public IDialogue[] dialogueOptions;

        public NpcData(CharacterName characterName, IDialogue[] dialogueOptions)
        {
            this.characterName = characterName;
            this.dialogueOptions = dialogueOptions;
        }
    }
}