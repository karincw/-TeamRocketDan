using UnityEngine;

namespace Karin.PoolingSystem
{

    public interface IPoolable
    {
        public PoolingType type { get; set; }
        public string name { get; set; }
        public GameObject GetGameObject();
        public void ResetItem();
        public void OnPush();
    }

}