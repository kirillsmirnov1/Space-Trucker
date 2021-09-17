using System;
using TMPro;
using UnityEngine;
using UnityUtils.View;

namespace Trucker.View.Dialogue
{
    public class DialogueViewEntry : ListViewEntry<string>
    {
        public static event Action<int> OnDialogueEntryClick; 
        
        [SerializeField] private TextMeshProUGUI text;
        
        public override void Fill(string data)
        {
            base.Fill(data);
            text.text = data;
        }

        public void OnClick()
        {
            OnDialogueEntryClick?.Invoke(transform.GetSiblingIndex());
        }
    }
}