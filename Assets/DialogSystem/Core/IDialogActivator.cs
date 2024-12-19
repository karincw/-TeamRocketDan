using System;
using UnityEngine;

namespace Karin.DialogSystem
{
    public interface IDialogActivator
    {
        public event Action<DialogType> PlayDialogEvent;
    }
}
