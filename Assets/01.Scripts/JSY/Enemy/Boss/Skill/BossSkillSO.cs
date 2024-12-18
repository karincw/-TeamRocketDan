using UnityEngine;

namespace JSY.Boss
{
    public abstract class BossSkillSO : ScriptableObject
    {
        protected Boss _owner;
        public void SetOwner(Boss owner)
        {
            _owner = owner;
        }
        public abstract void UseSkill(Transform target);
    }
}