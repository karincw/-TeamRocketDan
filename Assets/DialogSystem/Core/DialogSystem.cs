
using AYellowpaper.SerializedCollections;
using Karin.DialogSystem.Tree;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Karin.DialogSystem
{
    [RequireComponent(typeof(DialogCanvas))]
    public class DialogSystem : MonoBehaviour
    {
        public static bool IsPlayed = false;
        [SerializedDictionary("Type", "Dialog")]
        public SerializedDictionary<DialogType, DialogTree> dialogDictionary;
        [SerializeField] private BlackBoard _board = new();
        private DialogTree _currentDialog;
        public Action EndEvent;

        private void Awake()
        {
            IsPlayed = false;
            List<IDialogActivator> activators = new();
            activators = GetComponents<IDialogActivator>().ToList();
            activators.ForEach(activator =>
            {
                activator.PlayDialogEvent += HandlePlayDialog;
            });
            _board.owner = this;
            _board.canvas = GetComponent<DialogCanvas>();
        }

        private void OnDestroy()
        {
            List<IDialogActivator> activators = new();
            GetComponents<IDialogActivator>(activators);
            activators.ForEach(activator =>
            {
                activator.PlayDialogEvent -= HandlePlayDialog;
            });
        }

        private void Update()
        {
            if (IsPlayed)
            {
                _currentDialog.Update();
            }
        }

        private void HandlePlayDialog(DialogType type)
        {
            if (IsPlayed)
            {
                Debug.LogError("You cannot play dialogSystem at otherDialogSystem is running");
                return;
            }
            _currentDialog = dialogDictionary[type].Clone();
            _currentDialog.Bind(_board);
            IsPlayed = true;
        }

    }
}