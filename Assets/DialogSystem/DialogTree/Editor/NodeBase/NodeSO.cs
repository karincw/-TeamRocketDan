using UnityEngine;

namespace Karin.DialogSystem.Tree
{
    public enum NodeState
    {
        Running,
        Success,
        Failure
    }

    [CreateAssetMenu(fileName = "DialogNode", menuName = "SO/DialogNode")]
    public abstract class NodeSO : ScriptableObject
    {
        [HideInInspector] public NodeState state = NodeState.Running;
        public bool started = false;
        public string guid;
        public Vector2 position;
        public BlackBoard blackBoard;
        public DialogTree tree;

        /// <summary>
        /// Editor Only
        /// </summary>
        [HideInInspector] public NodeView view;

        public NodeState Update()
        {
            if (!started)
            {
                OnStart();
                started = true;
            }

            state = OnUpdate();

            if (state == NodeState.Failure || state == NodeState.Success)
            {
                OnStop();
                started = false;
            }

            return state;
        }

        public virtual NodeSO Clone()
        {
            return Instantiate(this);
        }

        protected abstract void OnStart();
        protected abstract NodeState OnUpdate();
        protected abstract void OnStop();
    }
}
