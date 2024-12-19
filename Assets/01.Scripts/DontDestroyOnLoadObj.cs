using System;
using UnityEngine;

public class DontDestroyOnLoadObj : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
