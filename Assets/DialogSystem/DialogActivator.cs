using System;
using UnityEngine;

namespace Karin.DialogSystem
{
    public class DialogActivator : MonoBehaviour, IDialogActivator
    {
        public DialogType dialogType = DialogType.Intro;
        public event Action<DialogType> PlayDialogEvent;

        private void Start()
        {
            Activate();
        }

        public void Activate()
        {
            PlayDialogEvent?.Invoke(dialogType);
            dialogType += 1;
        }
    }
}
