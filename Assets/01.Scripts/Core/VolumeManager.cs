using UnityEngine;
using UnityEngine.Rendering;

namespace Leo.Core
{
    public class VolumeManager : MonoSingleton<VolumeManager>
    {
        [SerializeField] private Volume _volume;
        
        public T GetComponent<T>() where T : VolumeComponent
        {
            if (_volume.profile.TryGet(out T component))
            {
                return component;
            }
            else
            {
                Debug.LogError($"VolumeManager: {typeof(T)} not found in volume profile.");
                return null;
            }
        }
    }
}