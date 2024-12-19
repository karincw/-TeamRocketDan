using DG.Tweening;
using TMPro;
using UnityEngine;

public class AttackText : MonoBehaviour
{
    [SerializeField] private TextMeshPro _text;
    public void SetText(string text)
    {
        _text.text = text;
        Sequence sequence = DOTween.Sequence()
            .Append(transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.2f))
            .Append(transform.DOScale(new Vector3(1, 1, 1), 0.2f)).SetEase(Ease.InSine).OnComplete(() => Destroy(gameObject));
        sequence.Play();
    }
}
