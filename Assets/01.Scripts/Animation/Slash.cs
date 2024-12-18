using System;
using DG.Tweening;
using UnityEngine;

namespace Leo.Animation
{
    public class Slash : MonoBehaviour
    {
        [SerializeField] private GameObject _visual;
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartSlash();
            }
        }

        [ContextMenu("StartSlash")]
        public void StartSlash()
        {
            transform.DOKill();
            transform.DORotate(new Vector3(0, 0, -90), 0f).OnComplete(() => _visual.SetActive(true));
            Sequence sequence = DOTween.Sequence();
            sequence.Append(transform.DORotate(new Vector3(0, 0, -90), 0.2f))
                .Join(transform.DORotate(new Vector3(0, 0, 0), 0.2f))
                .Join(transform.DORotate(new Vector3(0, 0, 90), 0.2f))
                .SetEase(Ease.Linear)
                .OnComplete(() => _visual.SetActive(false));
            
            sequence.Play();
        }
        
        #if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, 0.5f);
        }
        #endif
    }
}