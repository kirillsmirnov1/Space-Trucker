using TMPro;
using Trucker.Model.NPC;
using UnityEngine;
using UnityEngine.UI;
using UnityUtils;

namespace Trucker.View.Dialogue
{
    public class DialogueView : MonoBehaviour
    {
        [SerializeField] private GameObject fade;
        [SerializeField] private TextMeshProUGUI npcNameText;
        [SerializeField] private Image npcPortrait;

        private void OnValidate() 
            => this.CheckNullFields();

        private void Awake() 
            => DialogueProvider.OnDialogueInitiated += ShowDialogue;

        private void OnDestroy() 
            => DialogueProvider.OnDialogueInitiated -= ShowDialogue;

        private void ShowDialogue(string npcName, Sprite npcPicture, IDialogue[] dialogues)
        {
            npcNameText.text = npcName;
            npcPortrait.sprite = npcPicture;
            fade.gameObject.SetActive(true);
            LayoutRebuilder.ForceRebuildLayoutImmediate(npcNameText.rectTransform);
            // TODO use dialogue 
        }
    }
}