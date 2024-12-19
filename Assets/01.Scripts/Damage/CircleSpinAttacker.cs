using System.Collections.Generic;
using Leo.Interface;
using UnityEngine;

namespace Leo.Damage
{
    public class CircleSpinAttacker : MonoBehaviour, IEffectable, IColorChangeable
    {
        [SerializeField] private StarLite damageCaster;
        [SerializeField] private int _count;
        [SerializeField] private float _distance;
        [SerializeField] private int _speed;
        
        private List<StarLite> _starLites = new List<StarLite>();

        private void Update()
        {
            SpinAttack();
        }

        private void SpinAttack()
        {
            transform.Rotate(0, 0, 360 * Time.deltaTime * _speed);
        }

        public void SetPos(Transform target)
        {
            transform.position = target.position;
        }

        public void SetData(int distance, int count, int speed)
        {
            _distance = distance;
            _count = count;
            _speed = speed;
        }

        public void Play()
        {
            for (int i = 0; i < _count; i++)
            {
                var position = new Vector2(
                    transform.position.x + _distance * Mathf.Cos(2 * Mathf.PI / _count * i),
                    transform.position.y + _distance * Mathf.Sin(2 * Mathf.PI / _count * i));
                
                var star = Instantiate(
                    damageCaster,
                    position,
                    Quaternion.identity,
                    transform);
                Debug.Log("Play");
                _starLites.Add(star);
            }
        }

        public DamageCaster GetDamageCaster()
        {
            return damageCaster;
        }

        public void SetColor(Color color)
        {
            foreach (var star in _starLites)
            {
                Debug.Log("SetColor");
                star.SetColor(color);
            }
        }

        public void SetSize(float size)
        {
            foreach (var star in _starLites)
            {
                star.transform.localScale = new Vector3(size, size, 1);
            }
        }
    }
}