using JSY;
using System.Collections.Generic;
using UnityEngine;

namespace Leo.Entity.SO
{
    [CreateAssetMenu(fileName = "Wave", menuName = "SO/Wave")]
    public class WaveSO : ScriptableObject
    {
        public List<Enemy> enemies;
        public float spawnDelay;
        public int waveDelay;
    }
}