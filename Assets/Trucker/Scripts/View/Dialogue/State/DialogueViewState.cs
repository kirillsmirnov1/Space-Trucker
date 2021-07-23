namespace Trucker.View.Dialogue.State
{
    public abstract class DialogueViewState
    {
        protected DialogueView DialogueView;

        public DialogueViewState(DialogueView dialogueView)
            => DialogueView = dialogueView;

        public abstract void Start();
        public abstract void Stop();
        public abstract void OnDialogueEntryClick(int dialogueIndex);
    }
}