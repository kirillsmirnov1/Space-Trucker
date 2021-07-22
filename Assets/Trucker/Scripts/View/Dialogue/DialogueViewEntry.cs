using TMPro;
using Trucker.View.Util;
using UnityEngine;

namespace Trucker.View.Dialogue
{
    public class DialogueViewEntry : ListViewEntry<string>
    {
        [SerializeField] private TextMeshProUGUI text;
        // TODO button 
        // TODO id 
        
        public override void Fill(string data)
        {
            text.text = data;
        }
    }
}