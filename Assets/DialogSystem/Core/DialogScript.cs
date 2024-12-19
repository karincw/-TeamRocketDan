using UnityEngine;

namespace Karin.DialogSystem
{
    [System.Serializable]
    public struct DialogScript
    {
        [TextArea(1, 3)] public string outputText;
        public Charactor showCharacter;
    }
}
