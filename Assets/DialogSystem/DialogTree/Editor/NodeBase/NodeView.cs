using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace Karin.DialogSystem.Tree
{
    public class NodeView : Node
    {
        public NodeSO node;
        public Port input;
        public Port output;

        public Action<NodeView> OnNodeSelected;

        public NodeView(DialogNode node) : base("Assets/DialogSystem/DialogTree/Editor/UxmlElement/DialogNodeView.uxml")
        {
            this.node = node;
            this.node.view = this;
            this.title = node.name;
            this.viewDataKey = node.guid;
            style.left = node.position.x;
            style.top = node.position.y;
            CreateInputPorts();
            CreateOutputPorts();

            var dialogText = this.Q<TextField>("DialogText");
            dialogText.value = node.script.outputText;
            dialogText.RegisterValueChangedCallback<string>(HandleTextChange);
        }

        private void HandleTextChange(ChangeEvent<string> text)
        {
            (node as DialogNode).script.outputText = text.newValue;
        }

        public NodeView(NodeSO node) : base("Assets/DialogSystem/DialogTree/Editor/UxmlElement/NodeView.uxml")
        {
            this.node = node;
            this.node.view = this;
            this.title = node.name;
            this.viewDataKey = node.guid;
            style.left = node.position.x;
            style.top = node.position.y;
            CreateInputPorts();
            CreateOutputPorts();
        }

        private void CreateInputPorts()
        {
            if (node is DialogNode)
                input = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(bool));
            else if (node is ActionNode)
                input = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(bool));
            else if (node is EffectNode)
                input = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(bool));
            else if (node is EndNode)
                input = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(bool));

            if (input != null)
            {
                input.portName = string.Empty;
                inputContainer.Add(input);
            }
        }

        private void CreateOutputPorts()
        {
            if (node is DialogNode)
                output = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(bool));
            else if (node is ActionNode)
                output = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(bool));
            else if (node is EffectNode)
                input = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(bool));
            else if (node is RootNode)
                output = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(bool));

            if (output != null)
            {
                output.portName = string.Empty;
                outputContainer.Add(output);
            }
        }

        public override void SetPosition(Rect newPos)
        {
            base.SetPosition(newPos);
            node.position.x = newPos.xMin;
            node.position.y = newPos.yMin;
        }

        public override void OnSelected()
        {
            base.OnSelected();
            OnNodeSelected?.Invoke(this);
        }
    }
}