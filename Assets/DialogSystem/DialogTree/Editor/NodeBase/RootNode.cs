using UnityEngine;

namespace Karin.DialogSystem.Tree
{
    public class RootNode : NodeSO
    {
        public NodeSO child;

        protected override void OnStart()
        {
            Debug.Log("DialogStart");
        }

        protected override NodeState OnUpdate()
        {
            return child.Update();
        }

        protected override void OnStop()
        {
        }

        public override NodeSO Clone()
        {
            var node = Instantiate(this);
            node.child = child.Clone();
            return node;
        }
    }
}