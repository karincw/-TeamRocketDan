using Karin.DialogSystem;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace JSY
{
    public class NoTouchUI : MonoBehaviour
    {
        [SerializeField] private DialogSystem dialog;
        [SerializeField] private DialogActivator activator;
        [SerializeField] private List<GameObject> noTouchs;

        private void Awake()    
        {
            dialog.EndEvent += HandleEndEvent;
            activator.PlayDialogEvent += HandleStartEvent;
        }

        private void HandleStartEvent(DialogType type)
        {
            for(int i = 0; i < noTouchs.Count; ++i)
                noTouchs[i].SetActive(false);
        }

        private void HandleEndEvent()
        {
            noTouchs[(int)activator.dialogType-1].SetActive(true);
        }
    }
}
