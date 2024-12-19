using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace Karin.DialogSystem.Tree
{
    [UxmlElement]
    public partial class DialogView : GraphView
    {
        public Action<NodeView> OnNodeSelected;
        private DialogTree _tree;

        //[UxmlAttribute] 
        public DialogView()
        {
            VisualElement gridBG = new GridBackground();
            gridBG.name = "GridBackground";
            Insert(0, gridBG);

            var zoomer = new ContentZoomer();
            zoomer.maxScale = 5f;

            this.AddManipulator(zoomer);
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());
        }

        public void PopulateView(DialogTree tree)
        {
            _tree = tree;

            graphViewChanged -= OnGraphViewChanged;
            DeleteElements(graphElements);
            graphViewChanged += OnGraphViewChanged;

            if (!_tree.rootNode)
            {
                _tree.rootNode = tree.CreateNode(typeof(RootNode)) as RootNode;
                EditorUtility.SetDirty(tree);
                AssetDatabase.SaveAssets();
            }
            if (!_tree.endNode)
            {
                _tree.endNode = tree.CreateNode(typeof(EndNode)) as EndNode;
                EditorUtility.SetDirty(tree);
                AssetDatabase.SaveAssets();
            }

            tree.nodes.ForEach(n => CreateNodeView(n));

            tree.nodes.ForEach(n =>
            {
                var children = tree.GetChildren(n);
                NodeView parent = FindNodeView(n);
                foreach (var c in children)
                {
                    if (c == null)
                        continue;

                    NodeView child = FindNodeView(c);

                    Edge edge = parent.output.ConnectTo(child.input);
                    AddElement(edge);
                };
            });

        }

        private GraphViewChange OnGraphViewChanged(GraphViewChange graphViewChange)
        {
            if (graphViewChange.elementsToRemove != null)
            {
                graphViewChange.elementsToRemove.ForEach(elem =>
                {
                    var nv = elem as NodeView;
                    if (nv != null)
                    {
                        if (nv.node.GetType() != typeof(RootNode) && nv.node.GetType() != typeof(EndNode))
                            _tree.DeleteNode(nv.node);
                    }

                    var edge = elem as Edge;
                    if (edge != null)
                    {
                        NodeView parent = edge.output.node as NodeView;
                        NodeView child = edge.input.node as NodeView;

                        _tree.RemoveChild(parent.node, child.node);
                    }
                });
            }

            if (graphViewChange.edgesToCreate != null)
            {
                graphViewChange.edgesToCreate.ForEach(edge =>
                {
                    NodeView parent = edge.output.node as NodeView;
                    NodeView child = edge.input.node as NodeView;

                    _tree.AddChild(parent.node, child.node);
                });
            }

            return graphViewChange;
        }

        private NodeView FindNodeView(NodeSO node)
        {
            return GetNodeByGuid(node.guid) as NodeView;
        }

        public void CreateNodeView(NodeSO node)
        {
            NodeView nodeview;
            if (node is DialogNode dialogNode)
                nodeview = new NodeView(dialogNode);
            else
                nodeview = new NodeView(node);

            nodeview.OnNodeSelected = OnNodeSelected;
            AddElement(nodeview);
        }

        public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
        {
            CreateContextMenu<DialogNode>("Dialog", evt);
            CreateContextMenu<EffectNode>("Effect", evt);
            CreateContextMenu<ActionNode>("Action", evt);
        }

        private void CreateContextMenu<T>(string category, ContextualMenuPopulateEvent evt) where T : NodeSO
        {
            var types = TypeCache.GetTypesDerivedFrom<T>();

            Vector2 nodePosition = this.ChangeCoordinatesTo(contentViewContainer, evt.localMousePosition);

            foreach (Type type in types)
            {
                evt.menu.AppendAction($"{category}/{type.Name}", data => CreateNode(type, nodePosition));
            }
        }
        private void CreateNode(Type type, Vector2 position)
        {
            if (_tree == null)
            {
                Debug.LogError("Please Select DialogSO");
                return;
            }
            NodeSO node = _tree.CreateNode(type);
            node.position = position;
            CreateNodeView(node);
        }

        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            return ports.ToList()
                .Where(endPort => endPort.direction != startPort.direction && endPort.node != startPort.node).ToList();
        }

    }
}
