using Trucker.Model.NPC;
using UnityEngine;

namespace Trucker.View.Dialogue
{
    public class DialogueView : MonoBehaviour
    {
        [SerializeField] private GameObject fade;
        
        private void Awake() 
            => DialogueProvider.OnDialogueInitiated += ShowDialogue;

        private void OnDestroy() 
            => DialogueProvider.OnDialogueInitiated -= ShowDialogue;

        private void ShowDialogue(IDialogue[] dialogueOptions)
        {
            fade.gameObject.SetActive(true);
            // TODO use dialogue 
        }
    }
}