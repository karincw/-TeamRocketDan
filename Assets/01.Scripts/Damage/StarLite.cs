using Leo.Damage;
using UnityEngine;

public class StarLite : DamageCaster
{
    [field:SerializeField] public SpriteRenderer SrCompo { get; private set; }

    public void SetImage(Sprite sprite)
    {
        SrCompo.sprite = sprite;
    }
}
