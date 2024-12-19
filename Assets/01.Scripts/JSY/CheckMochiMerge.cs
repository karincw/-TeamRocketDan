using Karin;
using Karin.DialogSystem;
using UnityEngine;

public class CheckMochiMerge : MonoBehaviour
{
    [SerializeField] private MochiDataSO data;
    [SerializeField] private DialogActivator activator;
    private bool isTrue;
    void Update()
    {
        if (isTrue) return;

        if (transform.childCount > 0)
        {
            if(transform.GetChild(0).GetComponent<Mochi>().MochiData != data)
            {
                isTrue = true;
                activator.Activate();
            }
        }
    }
}
