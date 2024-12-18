using Mochi.Interface;
using UnityEngine;

namespace JSY.Boss
{
    public class Boss : MonoBehaviour
    {
        [SerializeField] private LayerMask _whatIsMochi;
        [SerializeField] private float _stunTime = 1f;
        
        private void TakeSkill(Transform target)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, target.position - transform.position, 10f, _whatIsMochi);
            if (hit.collider != null)
            {
                if (hit.collider.TryGetComponent(out IStunable stunable))
                {
                    stunable.Stun(_stunTime);
                }
            }
        }
    }
}