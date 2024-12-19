using Karin;
using UnityEngine;

public class SetUp : MonoBehaviour
{
    private Mochi mochi;

    private void Start()
    {
        mochi = GetComponent<Mochi>();
        mochi.SetUp();
    }
}
