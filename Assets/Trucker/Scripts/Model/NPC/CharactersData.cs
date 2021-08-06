using System;
using System.Linq;
using UnityEngine;
using UnityUtils.Attributes;

namespace Trucker.Model.NPC
{
    [CreateAssetMenu(fileName = "CharactersData", menuName = "Data/Characters Data", order = 0)]
    public class CharactersData : ScriptableObject
    {
        [NamedArray(typeof(CharacterData))]
        [SerializeField]
        private CharacterData[] characters;

        private void OnValidate() => ParseStringNames();

        private void ParseStringNames()
        {
            for (int i = 0; i < characters.Length; i++)
            {
                characters[i].nameText = characters[i].name.ToString();
            }
        }

        public CharacterData GetCharacterData(CharacterName charName) // IMPR use Dict
            => characters.First(ch => ch.name == charName);
    }

    [Serializable]
    public struct CharacterData
    {
        public CharacterName name;
        public string nameText;
        public Sprite portrait;
        public string description;
    }
}