using System.Collections.Generic;
using System.Linq;
using Trucker.Model.NPC;

namespace Trucker.View.Dialogue.State
{
    public class DialogueOptions : DialogueViewState
    {
        private readonly IDialogue[] _availableDialogues;

        public DialogueOptions(DialogueView dialogueView, IDialogue[] availableDialogues) : base(dialogueView) 
            => _availableDialogues = availableDialogues;

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
                DialogueView.SetStateDialogueInProgress(dialogue);
            }
        }

        private void SetDialogueOptions()
        {
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