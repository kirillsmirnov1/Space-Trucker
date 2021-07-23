using System.Collections.Generic;
using System.Linq;
using Trucker.Model.NPC;

namespace Trucker.View.Dialogue.State
{
    public class DialogueOptions : DialogueViewState
    {
        public DialogueOptions(DialogueView dialogueView) : base(dialogueView) { }

        public override void Start() 
            => SetDialogueOptions();  

        public override void Stop() { }

        public override void OnDialogueEntryClick(int dialogueIndex)
        {
            if (dialogueIndex >= DialogueView.AllDialogues.Length)
            {
                DialogueView.HideDialogue();
            }
            else
            {
                var dialogue = DialogueView.AllDialogues[dialogueIndex];
                DialogueView.SetState(new DialogueInProgress(DialogueView, dialogue));
            }
        }

        private void SetDialogueOptions()
        {
            var lines = GetFirstLines(DialogueView.AllDialogues); // IMPR may need to differ available/not available options 
            lines.Add("Bye."); // TODO customize 
            DialogueView.SetLines(lines);
        }

        private static List<string> GetFirstLines(IDialogue[] dialogues) // IMPR this is the best way???
        {
            return dialogues.Select(d => d.GetLines()[0]).ToList(); 
        }
    }
}