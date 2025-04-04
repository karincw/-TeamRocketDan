namespace Karin.DialogSystem.Tree
{
    public abstract class EffectNode : NodeSO
    {
        public NodeSO child;

        public override NodeSO Clone()
        {
            var node = Instantiate(this);
            node.child = child.Clone();
            return node;
        }
    }
}