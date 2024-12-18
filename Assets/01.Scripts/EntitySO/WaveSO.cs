using System.Collections.Generic;
using UnityEngine;

namespace Mochi.Entity.SO
{
    [CreateAssetMenu(fileName = "Wave", menuName = "SO/Wave")]
    public class WaveSO : ScriptableObject
    {
        public List<EnemySO> enemies;
        public float spawnInterval;
    }
}