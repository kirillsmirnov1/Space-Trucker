using OneLine;
using Trucker.Model.NPC;
using UnityEngine;

namespace Trucker.Model.Dialogues
{
    [CreateAssetMenu(menuName = "Quests/Dialogues/Oneliners", fileName = "Oneliners", order = 0)]
    public class Oneliners : ScriptableObject
    {
        [SerializeField, OneLine, HideLabel] private DialogueLine[] lines;

        private IDialogue[] _entries;
        
        public IDialogue[] Entries
        {
            get
            {
                if (_entries == null) SetEntries();
                return _entries;
            }
        }

        private void OnValidate() => SetEntries();

        private void SetEntries()
        {
            _entries = new IDialogue[lines.Length];
            for (var i = 0; i < lines.Length; i++)
            {
                var line = lines[i];
                _entries[i] = new OneLineDialogue {dialogueLine = line};
            }
        }

        private struct OneLineDialogue : IDialogue
        {
            public DialogueLine dialogueLine;

            public CharacterName InitialCharacter => dialogueLine.character;
            public DialogueLine[] Lines => new[] {dialogueLine};
            public string FirstLine => dialogueLine.line;
            public void OnDialogueEnd() { }
            public bool AvailableAsDialogueOption() => true;
        }

    }
}