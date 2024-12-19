using System;
using UnityEngine;

public class DontDestroyOnLoadObj : MonoSingleton<DontDestroyOnLoadObj>
{
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }
}
