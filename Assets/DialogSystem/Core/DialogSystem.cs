
using AYellowpaper.SerializedCollections;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Karin.DialogSystem
{
    [Serializable]
    public struct Texts
    {
        public List<string> texts;
    }

    [RequireComponent(typeof(DialogCanvas))]
    public class DialogSystem : MonoBehaviour
    {
        public static bool IsPlayed = false;
        [SerializedDictionary("Type", "Dialog")]
        public SerializedDictionary<DialogType, Texts> dialogDictionary;
        public Action EndEvent;
        [SerializeField] private Karin.DialogSystem.Tree.BlackBoard _board;
        [SerializeField] private List<GameObject> noTouchs;
        [SerializeField] private CanvasGroup group;

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
            group.blocksRaycasts = false;
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
        private void HandlePlayDialog(DialogType type)
        {
            if (IsPlayed)
            {
                Debug.LogError("You cannot play dialogSystem at otherDialogSystem is running");
                return;
            }
            StartCoroutine(PlayTextsCoroutine(type, dialogDictionary[type].texts));
        }

        public IEnumerator PlayTextsCoroutine(DialogType type, List<string> texts)
        {
            AllDisable();
            group.blocksRaycasts = true;
            foreach (string text in texts)
            {
                _board.canvas.SetDialogText(text, 1.3f, false);
                yield return new WaitForSeconds(2.3f);
            }
            _board.canvas.SetDialogText("", 0.01f, true, 0.3f);
            yield return new WaitForSeconds(0.3f);
            group.blocksRaycasts = false;
            HandleEndEvent(type);
        }

        private void AllDisable()
        {
            for (int i = 0; i < noTouchs.Count; ++i)
                noTouchs[i].SetActive(false);
        }

        private void HandleEndEvent(DialogType type)
        {
            AllDisable();
            int index = dialogDictionary.Values.ToList().FindIndex(x => x.Equals(dialogDictionary[type]));
            noTouchs[index].SetActive(true);
        }

    }
}