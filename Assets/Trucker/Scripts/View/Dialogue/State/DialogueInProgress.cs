using System.Collections.Generic;
using Trucker.Model.Dialogues;

namespace Trucker.View.Dialogue.State
{
    public class DialogueInProgress : DialogueViewState
    {
        private readonly IDialogue _dialogue;
        private int _lineIndex;
        private readonly DialogueLine[] _lines;

        public DialogueInProgress(DialogueView dialogueView, IDialogue dialogue) : base(dialogueView)
        {
            _dialogue = dialogue;
            _lines = _dialogue.Lines;
        }

        public override void Start() 
            => OnDialogueEntryClick(-1);

        public override void Stop() { }

        public override void OnDialogueEntryClick(int dialogueIndex)
        {
            _lineIndex++;
            if (_lineIndex < _lines.Length)
            {
                var line = _lines[_lineIndex];
                DialogueView.SetLines(new List<string> {line.line});
                DialogueView.SetCharacter(line.character);
            }
            else
            {
                _dialogue.OnDialogueEnd();
                DialogueView.SetStateDialogueOptions();
            }
        }
    }
}