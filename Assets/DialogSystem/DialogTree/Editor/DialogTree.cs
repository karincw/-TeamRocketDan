#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Karin.DialogSystem.Tree
{
    [CreateAssetMenu(fileName = "DialogTree", menuName = "SO/DialogSystem/Dialog")]
    public class DialogTree : ScriptableObject
    {
        public DialogPosition position;

        [HideInInspector] public NodeSO rootNode;
        [HideInInspector] public NodeSO endNode;
        public NodeState treeState = NodeState.Running;

        [HideInInspector] public List<NodeSO> nodes = new List<NodeSO>();
        [HideInInspector] public BlackBoard blackboard = new BlackBoard();

        public NodeState Update()
        {
            if (rootNode.state == NodeState.Running)
            {
                treeState = rootNode.Update();
            }
            return treeState;
        }

        public NodeSO CreateNode(System.Type type)
        {
            var node = ScriptableObject.CreateInstance(type) as NodeSO;
            node.name = type.Name;
            node.tree = this;
            node.guid = GUID.Generate().ToString();
            nodes.Add(node);

            AssetDatabase.AddObjectToAsset(node, this);
            AssetDatabase.SaveAssets();
            return node;
        }

        public void DeleteNode(NodeSO node)
        {
            nodes.Remove(node);
            AssetDatabase.RemoveObjectFromAsset(node);
            AssetDatabase.SaveAssets();
        }

        public void AddChild(NodeSO parent, NodeSO child)
        {
            if (parent is DialogNode dialogNode)
            {
                dialogNode.child = child;
            }
            else if (parent is EffectNode effectNode)
            {
                effectNode.child = child;
            }
            else if (parent is ActionNode actionNode)
            {
                actionNode.child = child;
            }
            else if (parent is RootNode rootNode)
            {
                rootNode.child = child;
            }
        }

        public void RemoveChild(NodeSO parent, NodeSO child)
        {
            if (parent is DialogNode dialogNode)
            {
                dialogNode.child = null;
            }
            else if (parent is EffectNode effectNode)
            {
                effectNode.child = null;
            }
            else if (parent is ActionNode actionNode)
            {
                actionNode.child = null;
            }
            else if (parent is RootNode rootNode)
            {
                rootNode.child = null;
            }
        }

        public List<NodeSO> GetChildren(NodeSO parent)
        {
            List<NodeSO> children = new List<NodeSO>();

            var dialog = parent as DialogNode;
            var action = parent as ActionNode;
            var effect = parent as EffectNode;
            var root = parent as RootNode;
            if (dialog != null)
            {
                children.Add(dialog.child);
            }
            else if (action != null && action.child != null)
            {
                children.Add(action.child);
            }
            else if (effect != null && effect.child != null)
            {
                children.Add(effect.child);
            }
            else if (root != null && root.child != null)
            {
                children.Add(root.child);
            }
            return children;
        }

        public void Traverse(NodeSO node, System.Action<NodeSO> visitor)
        {
            if (node)
            {
                visitor.Invoke(node);
                var children = GetChildren(node);
                children.ForEach(n => Traverse(n, visitor));
            }
        }

        public DialogTree Clone()
        {
            var tree = Instantiate(this);
            tree.rootNode = tree.rootNode.Clone();
            tree.nodes = new List<NodeSO>();
            Traverse(tree.rootNode, n =>
            {
                tree.nodes.Add(n);
            });
            return tree;
        }

        public void Bind(BlackBoard blackboard)
        {
            this.blackboard = blackboard;
            Traverse(rootNode, n =>
            {
                n.blackBoard = blackboard;
            });
        }

    }

#if UNITY_EDITOR
    [CustomEditor(typeof(DialogTree))]
    public class DialogTreeEditor : Editor
    {

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Open DialogTree"))
            {
                Selection.activeObject = target;
                DialogEditor.OpenWindow();
            }
        }
    }
#endif
}
#endif
