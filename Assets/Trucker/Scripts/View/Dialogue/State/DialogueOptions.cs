using System.Collections.Generic;
using System.Linq;
using Trucker.Model.NPC;

namespace Trucker.View.Dialogue.State
{
    public class DialogueOptions : DialogueViewState
    {
        private IDialogue[] _availableDialogues;
        public DialogueOptions(DialogueView dialogueView) : base(dialogueView) { }

        public override void Start() 
            => SetDialogueOptions();  

        public override void Stop() { }

        public override void OnDialogueEntryClick(int dialogueIndex)
        {
            if (dialogueIndex >= _availableDialogues.Length)
            {
                DialogueView.HideDialogue();
            }
            else
            {
                var dialogue = _availableDialogues[dialogueIndex];
                DialogueView.SetState(new DialogueInProgress(DialogueView, dialogue));
            }
        }

        private void SetDialogueOptions()
        {
            _availableDialogues = DialogueView.AllDialogues.Where(x => x.AvailableAsDialogueOption()).ToArray();
            var lines = GetFirstLines(_availableDialogues); 
            lines.Add("Bye."); // TODO customize 
            DialogueView.SetLines(lines);
        }

        private static List<string> GetFirstLines(IDialogue[] dialogues) // IMPR this is the best way???
        {
            return dialogues.Select(d => d.GetLines()[0]).ToList(); 
        }
    }
}