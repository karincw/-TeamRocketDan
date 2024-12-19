#if UNITY_EDITOR || PLATFORM_STANDALONE_WIN
using UnityEngine.UIElements;
namespace Karin.DialogSystem.Tree
{
    public abstract class DialogNode : NodeSO
    {
        public NodeSO child;
        public DialogScript script;

        public override NodeSO Clone()
        {
            var node = Instantiate(this);
            node.child = child.Clone();
            return node;
        }

#if  UNITY_EDITOR
        private void OnValidate()
        {
            if (view == null) return;

            var dialogText = view.Q<TextField>("DialogText");
            dialogText.value = script.outputText;
        }
#endif
    }
        
}
#endif