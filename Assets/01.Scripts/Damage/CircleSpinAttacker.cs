using Leo.Interface;
using UnityEngine;

namespace Leo.Damage
{
    public class CircleSpinAttacker : MonoBehaviour, IEffectable
    {
        [SerializeField] private StarLite damageCaster;
        [SerializeField] private int _count;
        [SerializeField] private float _distance;

        private void Update()
        {
            SpinAttack();
        }

        private void SpinAttack()
        {
            transform.Rotate(0, 0, 360 * Time.deltaTime);
        }

        public void SetPos(Transform target)
        {
            transform.position = target.position;
        }

        public void SetData(int distance, int count)
        {
            _distance = distance;
            _count = count;
        }

        public void Play()
        {
            for (int i = 0; i < _count; i++)
            {
                var position = new Vector2(
                    transform.position.x + _distance * Mathf.Cos(2 * Mathf.PI / _count * i),
                    transform.position.y + _distance * Mathf.Sin(2 * Mathf.PI / _count * i));
                
                Instantiate(
                    damageCaster,
                    position,
                    Quaternion.identity,
                    transform);
            }
        }

        public DamageCaster GetDamageCaster()
        {
            return damageCaster;
        }
    }
}