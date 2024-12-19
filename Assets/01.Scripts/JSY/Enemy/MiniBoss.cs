using Karin.PoolingSystem;
using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace JSY
{
    public class MiniBoss : Enemy
    {
        [SerializeField] private float _liftTime;
        [SerializeField] private TextMeshPro _lifeText;
        [SerializeField] private ParticleSystem _particleSystem;
        
        private float _timer;
        protected override void Start()
        {
            base.Start();
            transform.localScale = new Vector3(1.2f, 1.2f, 1f);
            _timer = _liftTime;
            //EnemyCountUI.Instance.UpdateCount(1);
            Destroy();
        }

        private void Destroy()
        {
            StartCoroutine(DestroyCoroutine());
        }

        private IEnumerator DestroyCoroutine()
        {
            yield return new WaitForSeconds(_liftTime);
            PoolManager.Instance.Push(this);
        }

        public void SetLifeText(int life)
        {
            _lifeText.text = life.ToString();
        }

        public override void ResetItem()
        {
            base.ResetItem();
            _timer = _liftTime;
        }

        protected override void Update()
        {
            base.Update();
            if (_timer <= 0)
            {
                var particles = Instantiate(_particleSystem, transform.position, Quaternion.identity);
                particles.Play();
            }
            _timer -= Time.deltaTime;
            SetLifeText(Mathf.RoundToInt(_timer));
        }
    }
}