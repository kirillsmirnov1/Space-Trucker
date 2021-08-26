using System;
using OneLine;
using Trucker.Model.Questing.Quests;
using UnityEngine;
using UnityUtils.Attributes;
using UnityUtils.Variables;

namespace Trucker.Model.NPC
{
    [CreateAssetMenu(fileName = "Dialogue", menuName = "Data/Dialogue", order = 0)]
    public class Dialogue : BoolVariable, IDialogue
    {
        [UnityUtils.Attributes.Separator("Dialogue")]
        [SerializeField] private bool canBeRepeated;
        [SerializeField] private CharacterName initialCharacter;
        [SerializeField, OneLine, HideLabel] private DialogueLine[] lines;
        
        [Header("Consequences")]
        [SerializeField] private DialogueType dialogueType;
        [SerializeField] 
        [ConditionalField("consequences", compareValues: new object[]{DialogueType.TakeQuest, DialogueType.FinishQuest})] 
        private Quest quest;

        public CharacterName InitialCharacter => initialCharacter;
        public DialogueLine[] Lines => lines;

        public string FirstLine => Lines[0].line;
        
        public void OnDialogueEnd()
        {
            Value = true;
            InvokeConsequences();
        }

        private void InvokeConsequences()
        {
            switch (dialogueType)
            {
                case DialogueType.Mini:
                case DialogueType.None:
                    break;
                case DialogueType.TakeQuest:
                    quest.Take();
                    break;
                case DialogueType.FinishQuest:
                    quest.Finish();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public bool AvailableAsDialogueOption() 
            => PassingConditions(); 

        private bool PassingConditions()
        {
            return dialogueType switch
            {
                DialogueType.None => DialogueWasNotShown || canBeRepeated,
                DialogueType.TakeQuest => quest.CanBeTaken && quest.NeverBeenStarted, 
                DialogueType.FinishQuest => quest.CanBeFinished,
                DialogueType.Mini => true,
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
            Mini,
        }
    }
}