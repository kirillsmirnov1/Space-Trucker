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
        {
            // FIXME that's fucking horrible 
            // TODO dialogue must reference giver? Or dialogues must be warped in CharacterClass of some sort 
            DialogueView.characterPortrait.SetPortrait(_availableDialogues[0].Lines.First(ch => ch.character != CharacterName.Fielding).character);
            SetDialogueOptions();
        }

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

        private static List<string> GetFirstLines(IDialogue[] dialogues) 
            => dialogues.Select(d => d.FirstLine).ToList();
    }
}