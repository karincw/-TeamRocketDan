using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance = null;
    private static bool IsDestoryed = false;

    public static T Instance
    {
        get
        {
            if (IsDestoryed)
            {
                _instance = null;
            }

            if (_instance == null)
            {
                _instance = GameObject.FindAnyObjectByType<T>();
                if (_instance == null)
                {
                    Debug.LogError($"{typeof(T).Name} singleton is not exists!");
                }
                else
                {
                    IsDestoryed = false;
                }
            }

            return _instance;
        }
    }

    private void OnDisable()
    {
        IsDestoryed = true;
    }
}
