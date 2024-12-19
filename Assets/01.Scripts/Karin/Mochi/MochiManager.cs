using AYellowpaper.SerializedCollections;
using JSY;
using System.Collections.Generic;
using UnityEngine;

namespace Karin
{
    public class MochiManager : MonoSingleton<MochiManager>
    {
        private Transform spawnPos;
        public SerializedDictionary<TowerRanking, List<MochiDataSO>> mochiDictionary;
        [SerializeField] private Mochi _mochiPrefab;

        public MochiDataSO GetNextMochi(TowerRanking rank)
        {
            var mochis = mochiDictionary[rank + 1];
            int randIdx = Random.Range(0, mochis.Count);
            return mochis[randIdx];
        }

        public void UpdateSpawnPos(Transform trm)
        {
            spawnPos = trm;
        }

        public Mochi InstantiateMochi(MochiDataSO data)
        {
            var mochi = Instantiate(_mochiPrefab, MochiMove.Instance.transform);
            mochi.transform.position = spawnPos.position;
            mochi.MochiData = data;
            mochi.SetUp();
            return mochi;
        }

        public Mochi InstantiateRandomMochi(TowerRanking rank)
        {
            return InstantiateMochi(GetNextMochi(rank));
        }
    }
}