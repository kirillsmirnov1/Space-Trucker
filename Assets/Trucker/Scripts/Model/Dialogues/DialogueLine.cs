using System;
using OneLine;
using Trucker.Model.NPC;

namespace Trucker.Model.Dialogues
{
    [Serializable]
    public struct DialogueLine
    {
        [Width(75)]
        public CharacterName character;
        public string line;
    }
}