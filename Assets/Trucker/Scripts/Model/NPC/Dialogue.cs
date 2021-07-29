using System;
using Trucker.Model.Questing.Quests;
using UnityEngine;
using UnityUtils.Attributes;
using UnityUtils.Variables;

namespace Trucker.Model.NPC
{
    [CreateAssetMenu(fileName = "Dialogue", menuName = "Data/Dialogue", order = 0)]
    public class Dialogue : BoolVariable, IDialogue
    {
        [SerializeField] private bool canBeRepeated;
        [SerializeField, TextArea(3, 20)] private string lines;
        
        [Header("Consequences")]
        [SerializeField] private DialogueType dialogueType;
        [SerializeField] 
        [ConditionalField("consequences", compareValues: new object[]{DialogueType.TakeQuest, DialogueType.FinishQuest})] 
        private Quest quest;

        private string[] _lines = null;

        public string[] Lines
        {
            get
            {
                if (_lines == null || _lines.Length == 0)
                {
                    _lines = lines.Split('\n');
                }
                return _lines;
            }
        }

        public string FirstLine => Lines[0];
        
        public void OnDialogueEnd()
        {
            Value = true;
            InvokeConsequences();
        }

        private void InvokeConsequences()
        {
            switch (dialogueType)
            {
                case DialogueType.None:
                    break;
                case DialogueType.TakeQuest:
                    quest.Take();
                    break;
                case DialogueType.FinishQuest:
                    quest.FinishQuest();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public bool AvailableAsDialogueOption() 
            => (DialogueWasNotShown || canBeRepeated) && PassingConditions(); 

        private bool PassingConditions()
        {
            return dialogueType switch
            {
                DialogueType.None => true,
                DialogueType.TakeQuest => quest.CanBeTaken,
                DialogueType.FinishQuest => quest.CanBeFinished,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private bool DialogueWasNotShown 
            => !Value;

        private bool DialogueWasShown 
            => Value;

        private enum DialogueType
        {
            None,
            TakeQuest,
            FinishQuest,
        }
    }
}