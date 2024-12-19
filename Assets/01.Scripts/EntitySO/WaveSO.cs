using Karin.PoolingSystem;
using System.Collections.Generic;
using UnityEngine;

namespace Leo.Entity.SO
{
    [CreateAssetMenu(fileName = "Wave", menuName = "SO/Wave")]
    public class WaveSO : ScriptableObject
    {
        public List<PoolingType> enemies;
        public bool isBoss;
        public int bossTimeLimit;
        public float spawnDelay;
        public int waveDelay;
    }
}