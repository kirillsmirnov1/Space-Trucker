using System.Collections.Generic;
using System.Linq;
using Trucker.Model.NPC;
using Trucker.Model.Questing.Steps.Goals;

namespace Trucker.View.Dialogue.State
{
    public class DialogueOptions : DialogueViewState
    {
        private readonly IDialogue[] _availableDialogues;

        public DialogueOptions(DialogueView dialogueView, IDialogue[] availableDialogues) : base(dialogueView) 
            => _availableDialogues = availableDialogues;

        public override void Start()
        {
            DialogueView.SetCharacter(CharacterToShow());
            Goal.OnCompletion += UpdateOptions;
            SetDialogueOptions();
        }

        private CharacterName CharacterToShow()
        {
            return _availableDialogues.Length > 0 
                ? _availableDialogues[0].InitialCharacter 
                : DialogueView.defaultCharacter;
        }

        public override void Stop()
        {
            Goal.OnCompletion -= UpdateOptions;
        }

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

        private void UpdateOptions(string obj) => DialogueView.SetStateDialogueOptions();
    }
}