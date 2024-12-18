using System.Collections.Generic;
using UnityEngine;

namespace Leo.Entity.SO
{
    [CreateAssetMenu(fileName = "Wave", menuName = "SO/Wave")]
    public class WaveSO : ScriptableObject
    {
        public List<EnemySO> enemies;
        public float spawnDelay;
        public float waveDelay;
    }
}