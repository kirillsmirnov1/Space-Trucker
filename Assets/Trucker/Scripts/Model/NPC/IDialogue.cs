namespace Trucker.Model.NPC
{
    public interface IDialogue
    {
        public string[] Lines { get; }
        public string FirstLine { get; }
        public void OnDialogueEnd();
        public bool AvailableAsDialogueOption();
    }
}