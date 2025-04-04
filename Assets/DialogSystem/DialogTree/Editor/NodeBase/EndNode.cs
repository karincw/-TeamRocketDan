#if UNITY_EDITOR
using UnityEngine;

namespace Karin.DialogSystem.Tree
{
    public class EndNode : NodeSO
    {
        public NodeSO child;

        protected override void OnStart()
        {
            Debug.Log("Dialog End");
            blackBoard.owner.EndEvent?.Invoke();
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
#endif