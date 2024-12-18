using DG.Tweening;
using NUnit.Framework.Constraints;
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Karin
{
    public class DragAndDrop : MonoSingleton<DragAndDrop>
    {
        [SerializeField] private DragAndDropObject _dragObject;
        [SerializeField] private InputReaderSO _inputReader;

        [Header("Selection")]
        [SerializeField] private float _pointRadius = 0.5f;
        [SerializeField] private LayerMask _dragLayer;
        private Vector3 _interpolationVector;

        [Header("Merge")]
        [SerializeField] private float _mergeRadius = 0.5f;

        [Space, Header("Debug")]
        [SerializeField] private bool _disableMergeDeleta;

        private void OnEnable()
        {
            _inputReader.LeftClickEvent += HandleLeftClick;
            _inputReader.LeftClickReleaseEvent += HandleLeftClickRelease;
        }

        private void OnDisable()
        {
            _inputReader.LeftClickEvent -= HandleLeftClick;
            _inputReader.LeftClickReleaseEvent -= HandleLeftClickRelease;
        }

        private void Update()
        {
            if (_dragObject == null) return;

            Camera cam = Camera.main;
            Vector3 mousePos = cam.ScreenToWorldPoint(Mouse.current.position.value);

            _dragObject.gameObject.transform.position = mousePos + _interpolationVector;
        }

        private void HandleLeftClick()
        {
            Camera cam = Camera.main;
            Vector3 mousePos = cam.ScreenToWorldPoint(Mouse.current.position.value);
            Collider2D col = Physics2D.OverlapCircle(mousePos, _pointRadius, _dragLayer);

            if (col == null) return;
            _dragObject = col.attachedRigidbody.GetComponent<DragAndDropObject>();
            _interpolationVector = _dragObject.transform.position - mousePos;
            _interpolationVector.z = 10;
            _dragObject.ColliderEnable(false);
            _dragObject.isDrag = true;
        }

        private void HandleLeftClickRelease()
        {
            if (_dragObject == null) return;
            _dragObject.ColliderEnable(true);
            _dragObject.isDrag = false;
            MergeMochi();
            _dragObject.VaildCheck();
            _dragObject = null;
        }

        private void MergeMochi()
        {
            Camera cam = Camera.main;
            Vector3 mousePos = cam.ScreenToWorldPoint(Mouse.current.position.value);
            mousePos.z = 0;

            Collider2D[] cols = Physics2D.OverlapCircleAll(mousePos, _mergeRadius, _dragLayer);
            Mochi mochi = null;
            foreach (var col in cols)
            {
                if (col.attachedRigidbody.gameObject != _dragObject.gameObject)
                {
                    var m = col.attachedRigidbody.GetComponent<DragAndDropObject>();
                    var otherMochi = m as Mochi;
                    if (otherMochi.MochiData.ranking == (_dragObject as Mochi).MochiData.ranking)
                    {
                        mochi = otherMochi;
                        break;
                    }
                }
            }
            if (mochi != null)
            {
                var newMochi = MochiManager.Instance.InstantiateRandomMochi(mochi.MochiData.ranking);

                if (!_disableMergeDeleta)
                {
                    Destroy(mochi.gameObject);
                    Destroy(_dragObject.gameObject);
                }

                newMochi.transform.position = mousePos;
            }

        }
    }
}