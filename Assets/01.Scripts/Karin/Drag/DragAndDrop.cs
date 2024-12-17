using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Karin
{
    public class DragAndDrop : MonoSingleton<DragAndDrop>
    {
        [SerializeField] private DragAndDropObject _dragObject;
        [SerializeField] private InputReaderSO _inputReader;

        private void OnEnable()
        {
            _inputReader.LeftClickEvent += HandleLeftClick;
        }

        private void OnDisable()
        {
            _inputReader.LeftClickEvent -= HandleLeftClick;
        }

        private void HandleLeftClick()
        {
            Camera cam = Camera.main;
            Vector3 mousePos = cam.ScreenToWorldPoint(Mouse.current.position.value);
        }
    }
}