namespace Trucker.Model.NPC
{
    public interface IDialogue
    {
        public DialogueLine[] Lines { get; }
        public string FirstLine { get; }
        public void OnDialogueEnd();
        public bool AvailableAsDialogueOption();
    }
}