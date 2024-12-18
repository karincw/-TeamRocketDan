using UnityEngine;

namespace Karin.PoolingSystem
{

    public interface IPoolable
    {
        public string ItemName { get; set; }
        public GameObject GetGameObject();
        public void ResetItem();
    }

}