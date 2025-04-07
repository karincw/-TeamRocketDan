using Karin.PoolingSystem;
using Leo.Sound;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace Karin
{
    public class DragAndDrop : MonoSingleton<DragAndDrop>
    {
        [SerializeField] private SoundObject _soundObject;
        [SerializeField] private DragAndDropObject _dragObject;
        [SerializeField] private InputReaderSO _inputReader;

        [Header("Selection")]
        [SerializeField] private float _pointRadius = 0.5f;
        [SerializeField] private LayerMask _dragLayer;
        private Vector3 _interpolationVector;

        [Header("Merge")]
        [SerializeField] private float _mergeRadius = 0.5f;
        [SerializeField] private ParticleSystem _mergeEffect;

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

            _dragObject.gameObject.transform.position = GetInputPosition() + _interpolationVector;
        }

        private void HandleLeftClick()
        {
            Camera cam = Camera.main;
            Vector3 mousePos = GetInputPosition();
            Collider2D col = Physics2D.OverlapCircle(mousePos, _pointRadius, _dragLayer);

            if (col == null) return;
            _dragObject = col.attachedRigidbody.GetComponent<DragAndDropObject>();
            _interpolationVector = _dragObject.transform.position - mousePos;
            _interpolationVector.z = 10;
            _dragObject.ColliderTrigger(true);
            _dragObject.ShowRadius(true);
            _dragObject.isDrag = true;
        }

        private Vector3 GetInputPosition()
        {
            Vector3 inputPos;

            if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed)
                inputPos = Touchscreen.current.primaryTouch.position.value;
            else
                inputPos = Mouse.current.position.value;

            return Camera.main.ScreenToWorldPoint(new Vector3(inputPos.x, inputPos.y, 10));
        }

        private void HandleLeftClickRelease()
        {
            if (_dragObject == null) return;
            _dragObject.ColliderTrigger(false);
            _dragObject.ShowRadius(false);
            _dragObject.isDrag = false;
            MergeMochi();
            _dragObject.VaildCheck();
            _dragObject = null;
        }

        private void MergeMochi()
        {
            if ((_dragObject as Mochi).MochiData.ranking == TowerRanking.five)
                return;
            Camera cam = Camera.main;
            Vector3 mousePos = GetInputPosition();

            Collider2D[] cols = Physics2D.OverlapCircleAll(_dragObject.transform.position, _mergeRadius, _dragLayer);
            Mochi mochi = null;
            foreach (var col in cols)
            {
                if (col.attachedRigidbody.gameObject != _dragObject.gameObject)
                {
                    var m = col.attachedRigidbody.GetComponent<DragAndDropObject>();
                    var otherMochi = m as Mochi;
                    if (otherMochi.MochiData == (_dragObject as Mochi).MochiData)
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
                    PoolManager.Instance.Push(mochi);
                    PoolManager.Instance.Push(_dragObject as Mochi);
                    _mergeEffect.Play();
                    _soundObject?.Play();
                }

                newMochi.transform.position = _dragObject.transform.position;
            }

        }
    }
}