using System.Collections.Generic;
using UnityEngine;

namespace Karin.PoolingSystem
{

    public class Pool
    {
        private Stack<IPoolable> _pool;
        private Transform _parent;
        private IPoolable _poolable;
        private GameObject _prefab;

        public Pool(IPoolable poolable, Transform parent, int count)
        {
            _pool = new Stack<IPoolable>(count);
            _parent = parent;
            _poolable = poolable;
            _prefab = poolable.GetGameObject();
            // for (int i = 0; i < count; i++)
            // {
            //     GameObject gameObj = GameObject.Instantiate(_prefab, _parent);
            //     gameObj.SetActive(false);
            //     IPoolable item = gameObj.GetComponent<IPoolable>();
            //     item.type = _poolable.type;
            //     _pool.Push(item);
            // }
        }

        public IPoolable Pop()
        {
            IPoolable item = null;
            GameObject gameObj = GameObject.Instantiate(_prefab, _parent);
            item = gameObj.GetComponent<IPoolable>();
            item.type = _poolable.type;

            return item;
        }

        public void Push(IPoolable item)
        {
            item.OnPush();
            Object.Destroy(item.GetGameObject());
            _pool.Push(item);
        }
    }

}