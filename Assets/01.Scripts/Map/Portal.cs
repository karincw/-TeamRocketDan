using UnityEngine;

namespace Mochi.Map
{
    public class Portal : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            other.transform.position = _target.position;
        }
    }
}