using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Karin
{
    public class Mochi : DragAndDropObject
    {
        [SerializeField] private MochiDataSO _mochiData;
        public MochiDataSO MochiData { get => _mochiData; set => _mochiData = value; }

        private List<MochiCompo> _mochiCompos;

        [ContextMenu("SetUp")]
        public void SetUp()
        {
            _mochiCompos = GetComponentsInChildren<MochiCompo>().ToList();
            _mochiCompos.ForEach(compo =>
            {
                compo.SetUp(); 
            });
        }

    }
}