using AYellowpaper.SerializedCollections;
using JSY;
using Karin.PoolingSystem;
using System.Collections.Generic;
using UnityEngine;

namespace Karin
{
    public class MochiManager : MonoSingleton<MochiManager>
    {
        private Transform spawnPos;
        public SerializedDictionary<TowerRanking, List<MochiDataSO>> mochiDictionary;
        [SerializeField] private Mochi _mochiPrefab;

        protected override void Awake()
        {
            UpdateSpawnPos(transform);
        }
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
            var mochi = PoolManager.Instance.Pop(PoolingType.Tower_MochiBase) as Mochi;
            mochi.transform.position = spawnPos.position;
            mochi.MochiData = data;
            mochi.transform.parent = MochiMove.Instance.transform;
            mochi.SetUp();
            return mochi;
        }

        public Mochi InstantiateRandomMochi(TowerRanking rank)
        {
            return InstantiateMochi(GetNextMochi(rank));
        }
    }
}