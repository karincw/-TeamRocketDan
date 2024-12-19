using System;
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
            Destroy(gameObject, _liftTime);
        }
        
        public void SetLifeText(int life)
        {
            _lifeText.text = life.ToString();
        }

        protected override void Update()
        {
            base.Update();
            _timer -= Time.deltaTime;
            SetLifeText(Mathf.RoundToInt(_timer));
        }

        private void OnDestroy()
        {
            var particles = Instantiate(_particleSystem, transform.position, Quaternion.identity);
            particles.Play();
        }
    }
}