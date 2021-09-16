using Trucker.Model.NPC;

namespace Trucker.Model.Dialogues
{
    public interface IDialogue
    {
        public CharacterName InitialCharacter { get; }
        public DialogueLine[] Lines { get; }
        public string FirstLine { get; }
        public void OnDialogueEnd();
        public bool AvailableAsDialogueOption();
    }
}