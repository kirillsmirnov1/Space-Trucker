using Trucker.Model.NPC;
using UnityEngine;
using UnityEngine.UI;

namespace Trucker.View.Dialogue
{
    public class CharacterPortrait : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private CharactersData characterData;
        
        private CharacterName _lastCharacter;

        public void SetPortrait(CharacterName character)
        {
            if(character == _lastCharacter) return;
            image.sprite = characterData.GetCharacterData(character).portrait; // IMPR animation 
            _lastCharacter = character;
        }
        
    }
}