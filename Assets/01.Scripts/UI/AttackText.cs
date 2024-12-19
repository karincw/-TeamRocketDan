using DG.Tweening;
using TMPro;
using UnityEngine;

public class AttackText : MonoBehaviour
{
    [SerializeField] private TextMeshPro _text;
    public void SetText(string text)
    {
        _text.text = text;
        transform.DOScale(new Vector3(1,1,1), 0.2f).SetEase(Ease.InBounce).OnComplete(() => Destroy(gameObject));
    }
}
