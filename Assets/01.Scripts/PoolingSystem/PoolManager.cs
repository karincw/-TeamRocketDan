using System.Collections.Generic;
using UnityEngine;

namespace Karin.PoolingSystem
{

    public class PoolManager : MonoSingleton<PoolManager>
    {
        [SerializeField] private PoolingListSO _poolList;

        private Dictionary<PoolingType, Pool> _pools;

        private void Awake()
        {
            _pools = new Dictionary<PoolingType, Pool>();

            foreach (PoolItemSO item in _poolList.list)
            {
                CreatePool(item.type, item.prefab, item.count);
            }
        }


        private void CreatePool(PoolingType type, GameObject prefab, int count)
        {
            IPoolable poolable = prefab.GetComponent<IPoolable>();
            if (poolable == null)
            {
                //this gameobject does not has poolable interface
                Debug.LogWarning($"Gameobject {prefab.name} has not Ipoolable script");
                return;
            }

            poolable.type = type;
            Pool pool = new Pool(poolable, transform, count);
            _pools.Add(poolable.type, pool);
        }

        public IPoolable Pop(PoolingType type)
        {
            if (_pools.ContainsKey(type))
            {
                IPoolable item = _pools[type].Pop();

                item.ResetItem();
                return item;
            }
            Debug.LogError($"There is no pool {type.ToString()}");
            return null;
        }

        public void Push(IPoolable item)
        {
            if (_pools.ContainsKey(item.type))
            {
                _pools[item.type].Push(item);
                return;
            }
            Debug.LogError($"There is no pool {item.name}");
        }
    }

}