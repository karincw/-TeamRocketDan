using AYellowpaper.SerializedCollections;
using System.Collections.Generic;
using UnityEngine;

namespace Karin
{
    public class MochiManager : MonoSingleton<MochiManager>
    {
        public SerializedDictionary<TowerRanking, MochiDataSO> mochiDictionary;
    }
}