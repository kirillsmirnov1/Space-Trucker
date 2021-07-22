using System.Linq;
using UnityEngine;

namespace Trucker.Model.NPC
{
    public class DialogueProvider : MonoBehaviour
    {
        [SerializeField] private ScriptableObject[] dialogueOptions; // Unity still doesnt serialize interfaces, so here we go 

        private IDialogue[] GetDialogues() 
            => dialogueOptions.Select(x => (IDialogue) x).ToArray();
    }
}