using System;
using UnityEditor;
using UnityEngine;

namespace Karin.DialogSystem
{
#if UNITY_EDITOR

    public class DebugDialogActivator : MonoBehaviour, IDialogActivator
    {
        public DialogType debuggingType;
        public event Action<DialogType> PlayDialogEvent;

        [ContextMenu("Activate")]
        public void Activate()
        {
            PlayDialogEvent?.Invoke(debuggingType);
        }
    }

    [CustomEditor(typeof(DebugDialogActivator))]
    public class DebugDialogActivatorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("Activate Button"))
            {
                (target as DebugDialogActivator).Activate();
            }
        }
    }

#endif
}
