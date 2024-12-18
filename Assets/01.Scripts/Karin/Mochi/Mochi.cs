using Leo.Interface;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Karin
{
    public class Mochi : DragAndDropObject, IStunable
    {
        [SerializeField] private MochiDataSO _mochiData;
        public MochiDataSO MochiData { get => _mochiData; set => _mochiData = value; }

        private List<MochiCompo> _mochiCompos;
        public bool canAttack = true;

        [ContextMenu("SetUp")]
        public void SetUp()
        {
            _mochiCompos = GetComponentsInChildren<MochiCompo>().ToList();
            _mochiCompos.ForEach(compo =>
            {
                compo.SetUp();
            });
        }

        public override void ShowRadius(bool state)
        {
            _mochiCompos.ForEach(compo =>
            {
                if (compo is MochiVisualLoader loader)
                {
                    loader.SetAttackDistance(state);
                }
            });
        }

        public void Stun(float duration)
        {
            StartCoroutine(StunCoroutine(duration));
        }

        private IEnumerator StunCoroutine(float time)
        {
            canAttack = false;
            yield return new WaitForSeconds(time);
            canAttack = true;
        }
    }
}