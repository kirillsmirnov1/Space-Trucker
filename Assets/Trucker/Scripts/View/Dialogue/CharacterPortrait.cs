using UnityEngine;
using UnityEngine.UI;

namespace Trucker.View.Dialogue
{
    public class CharacterPortrait : MonoBehaviour
    {
        [SerializeField] private Image image;
        
        public void SetPortrait(Sprite portrait)
        {
            image.sprite = portrait;
        }
        
    }
}