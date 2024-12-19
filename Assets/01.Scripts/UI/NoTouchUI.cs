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

        private void HandleStartEvent()
        {
            for(int i = 0; i < noTouchs.Count; ++i)
                noTouchs[i].SetActive(false);
        }

        private void HandleEndEvent()
        {
            HandleStartEvent();
            noTouchs[(int)activator.dialogType].SetActive(true);
        }
    }
}
