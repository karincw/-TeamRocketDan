
using UnityEngine;

namespace Karin.DialogSystem.Tree
{
    [System.Serializable]
    public class BlackBoard
    {
        public DialogCanvas canvas;
        public AudioSource audioSource;
        public DialogSystem owner;
    }
}