using TMPro;
using UnityEngine;

namespace Karin.DialogSystem.Tree
{
    public class ShakeNode : EffectNode
    {
        public int effectCount = 0;

        protected override void OnStart()
        {
            TMP_Text tmpText = blackBoard.canvas.GetCurrentText(tree.position);
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
