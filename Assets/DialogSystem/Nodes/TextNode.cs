using DG.Tweening;
using UnityEditor;
using UnityEngine;

namespace Karin.DialogSystem.Tree
{
    public class TextNode : DialogNode
    {
        private float startTime;
        public float time = 1f;
        public float delaytime = 1f;
        public bool textSkip = false;
        public bool dialogSkip = false;

        protected override void OnStart()
        {
            if (tree.position == DialogPosition.OnHead)
            {
                blackBoard.canvas.SetHeadText(script.outputText, time, child is EndNode);
            }
            else if(tree.position == DialogPosition.UnderBar)
            {
                blackBoard.canvas.SetDialogText(script.outputText, time, child is EndNode);
            }
            startTime = Time.time;
            textSkip = false; 
            dialogSkip = false;
        }

        protected override void OnStop()
        {

        }

        protected override NodeState OnUpdate()
        {
            if (dialogSkip || Time.time - startTime > time + delaytime)
            {
                return child.Update();
            }
            return NodeState.Running;
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(TextNode))]
    public class TextNodeEditor : Editor
    {
        private SerializedProperty _scriptProp;
        private SerializedProperty _timeProp;
        private SerializedProperty _delayProp;

        private void OnEnable()
        {
            _scriptProp = serializedObject.FindProperty("script");
            _timeProp = serializedObject.FindProperty("time");
            _delayProp = serializedObject.FindProperty("delaytime");
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.PropertyField(_scriptProp);
            EditorGUILayout.PropertyField(_timeProp);
            EditorGUILayout.PropertyField(_delayProp);

            serializedObject.ApplyModifiedProperties();
        }
    }
#endif
}
