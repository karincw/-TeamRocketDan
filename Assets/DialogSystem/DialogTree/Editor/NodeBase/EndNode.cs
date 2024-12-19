
using UnityEngine;

namespace Karin.DialogSystem.Tree
{
    public class EndNode : NodeSO
    {
        public NodeSO child;

        protected override void OnStart()
        {
            Debug.Log("Dialog End");
            DialogSystem.IsPlayed = false;
        }

        protected override NodeState OnUpdate()
        {
            return NodeState.Success;
        }

        protected override void OnStop()
        {
        }

        public override NodeSO Clone()
        {
            var node = Instantiate(this);
            return node;
        }
    }
}
