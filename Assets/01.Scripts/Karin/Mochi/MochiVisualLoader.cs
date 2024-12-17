using UnityEngine;

namespace Karin
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class MochiVisualLoader : MonoBehaviour
    {
        private Mochi _owner;
        private SpriteRenderer _renderer;

        private void Awake()
        {
            _owner = GetComponentInParent<Mochi>();
            _renderer = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            _renderer.sprite = _owner.MochiData.image;
        }
    }
}