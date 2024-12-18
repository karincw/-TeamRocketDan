using System.Collections.Generic;
using UnityEngine;

namespace Karin.PoolingSystem
{

    public class PoolManager : MonoSingleton<PoolManager>
    {
        [SerializeField] private PoolingListSO _poolList;

        private Dictionary<string, Pool> _pools;

        private void Awake()
        {
            _pools = new Dictionary<string, Pool>();

            foreach (PoolItemSO item in _poolList.list)
            {
                CreatePool(item.itemName, item.prefab, item.count);
            }
        }


        private void CreatePool(string itemName, GameObject prefab, int count)
        {
            IPoolable poolable = prefab.GetComponent<IPoolable>();
            if (poolable == null)
            {
                //this gameobject does not has poolable interface
                Debug.LogWarning($"Gameobject {prefab.name} has not Ipoolable script");
                return;
            }

            poolable.ItemName = itemName;
            Pool pool = new Pool(poolable, transform, count);
            _pools.Add(poolable.ItemName, pool);
        }

        public IPoolable Pop(string itemName)
        {
            if (_pools.ContainsKey(itemName))
            {
                IPoolable item = _pools[itemName].Pop();

                item.ResetItem();
                return item;
            }
            Debug.LogError($"There is no pool {itemName}");
            return null;
        }

        public void Push(IPoolable item)
        {
            if (_pools.ContainsKey(item.ItemName))
            {
                _pools[item.ItemName].Push(item);
                return;
            }
            Debug.LogError($"There is no pool {item.ItemName}");
        }
    }

}