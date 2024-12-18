using System;
using DG.Tweening;
using Mochi.Core;
using UnityEngine;

namespace Mochi.Effect
{
    public class Blaster : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private GameObject _visual;

        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Shot();
            }
        }

        public void Shot()
        {
            _visual.SetActive(true);
            transform.DOComplete();
            transform.localScale = new Vector3(0, 1, 1);
            _particleSystem.Play();
            Sequence sequence = DOTween.Sequence();
            sequence.Append(transform.DOScaleX(1f, 0.1f))
                .AppendInterval(5f)
                .OnComplete(() => _visual.SetActive(false));
            CameraManager.Instance.ShakeCamera(0.1f, 5f);
        }
    }
}