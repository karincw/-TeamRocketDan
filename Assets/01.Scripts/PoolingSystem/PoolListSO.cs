using System.Collections.Generic;
using UnityEngine;

namespace Karin.PoolingSystem
{

    [CreateAssetMenu(menuName = "SO/Pool/List")]
    public class PoolingListSO : ScriptableObject
    {
        public List<PoolItemSO> list;
    }

}