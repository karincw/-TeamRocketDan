using System.Collections.Generic;
using UnityEngine;

namespace Mocchi.Entity.SO
{
    [CreateAssetMenu(fileName = "Wave", menuName = "EntitySO/Wave")]
    public class WaveSO : ScriptableObject
    {
        public List<EnemySO> enemies;
    }
}