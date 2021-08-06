using System;
using OneLine;

namespace Trucker.Model.NPC
{
    [Serializable]
    public struct DialogueLine
    {
        [Width(75)]
        public CharacterName character;
        public string line;
    }
}