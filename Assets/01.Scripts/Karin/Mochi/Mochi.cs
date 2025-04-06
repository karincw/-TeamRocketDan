using Karin.PoolingSystem;
using Leo.Animation;
using Leo.Interface;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Karin
{
    public class Mochi : DragAndDropObject, IStunable, IPoolable
    {
        [SerializeField] private MochiDataSO _mochiData;
        public MochiDataSO MochiData { get => _mochiData; set => _mochiData = value; }
        [field: SerializeField] public PoolingType type { get; set; }

        private List<MochiCompo> _mochiCompos;
        public bool CanAttack => !isStun && !moving;
        public bool isStun = false;
        private bool moving = false;

        [Space, Header("Stun")]
        [SerializeField] private EllipticalMotion stunEffect;
        [SerializeField] private Vector3 _interpolatePosition;

        [ContextMenu("SetUp")]
        public void SetUp()
        {
            _mochiCompos = GetComponentsInChildren<MochiCompo>().ToList();
            _mochiCompos.ForEach(compo =>
            {
                compo.SetUp();
            });
            stunEffect.gameObject.SetActive(false);
        }

        public override void ShowRadius(bool state)
        {
            moving = state;
            _mochiCompos.ForEach(compo =>
            {
                if (compo is MochiVisualLoader loader)
                {
                    loader.SetAttackDistance(state);
                }
            });

        }

        [ContextMenu("stunTest")]
        private void TestStun()
        {
            Stun(3f);
        }

        public void Stun(float duration)
        {
            StartCoroutine(StunCoroutine(duration));
        }

        private IEnumerator StunCoroutine(float time)
        {
            stunEffect.SetCenter(_interpolatePosition);
            stunEffect.gameObject.SetActive(true);
            isStun = true;
            yield return new WaitForSeconds(time);
            isStun = false;
            stunEffect.gameObject.SetActive(false);
        }


        public GameObject GetGameObject()
        {
            return gameObject;
        }

        public void ResetItem()
        {
            // Pop
        }

        public void OnPush()
        {
            _mochiCompos.ForEach(compo =>
            {
                compo.Release();
            });
            transform.parent = PoolManager.Instance.transform;
        }
    }
}