using AYellowpaper.SerializedCollections;
using System.Collections.Generic;
using UnityEngine;

namespace Karin
{
    public class MochiManager : MonoSingleton<MochiManager>
    {
        public SerializedDictionary<TowerRanking, List<MochiDataSO>> mochiDictionary;
        [SerializeField] private Mochi _mochiPrefab;

        public MochiDataSO GetNextMochi(TowerRanking rank)
        {
            var mochis = mochiDictionary[rank + 1];
            int randIdx = Random.Range(0, mochis.Count);
            return mochis[randIdx];
        }

        public Mochi InstantiateMochi(MochiDataSO data)
        {
            var mochi = Instantiate(_mochiPrefab);
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