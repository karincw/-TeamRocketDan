using UnityEngine;

namespace JSY.Boss
{
    public abstract class BossSkillSO : ScriptableObject
    {
        public abstract void UseSkill(Transform target);
    }
}