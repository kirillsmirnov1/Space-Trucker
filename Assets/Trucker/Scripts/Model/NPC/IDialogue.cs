namespace Trucker.Model.NPC
{
    public interface IDialogue
    {
        public string[] GetLines();
        public void OnDialogueEnd();
        public bool AvailableAsDialogueOption();
    }
}