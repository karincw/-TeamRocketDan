using System;
using System.Collections;
using DG.Tweening;
using Leo.Damage;
using Leo.Interface;
using UnityEngine;

namespace Leo.Animation
{
    public class Slash : MonoBehaviour, IEffectable, IColorChangeable
    {
        [SerializeField] private GameObject _visual;
        [SerializeField] private TrailRenderer _trailRenderer;
        private Material _material;
        private void Awake()
        {
            _material = Instantiate(_trailRenderer.material);
            _trailRenderer.material = _material;
        }

        public void SetColor(Color color)
        {
            _material.SetColor("_MainColor", color);
        }

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

            StartCoroutine(SlashEffect());
        }

        private IEnumerator SlashEffect()
        {
            _visual.SetActive(false);
            _visual.transform.localPosition = new Vector3(1f, 1f);
            yield return null;
            _visual.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            _visual.transform.localPosition = new Vector3(-1f, -1f);
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, 0.5f);
        }
#endif
        public void SetPos(Transform target)
        {
            transform.position = target.position;
        }

        public void Play()
        {
            StartSlash();
        }

        public DamageCaster GetDamageCaster()
        {
            return null;
        }
    }
}