using UnityEngine;

namespace JSY
{
    public class EnemyHealthBar : MonoBehaviour
    {
        [SerializeField] private Transform _bar;
        
        public void SetHealthBar(float health)
        {
            _bar.localScale = new Vector3(health, 1, 1);
        }
    }
}