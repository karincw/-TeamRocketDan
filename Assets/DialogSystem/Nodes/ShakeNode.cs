#if UNITY_EDITOR
using TMPro;
using UnityEngine;

namespace Karin.DialogSystem.Tree
{
    public class ShakeNode : EffectNode
    {
        public int effectCount = 0;

        protected override void OnStart()
        {
            //tmpText.mesh
        }

        protected override NodeState OnUpdate()
        {
            return child.Update();
        }

        protected override void OnStop()
        {

        }
    }
}
#endif