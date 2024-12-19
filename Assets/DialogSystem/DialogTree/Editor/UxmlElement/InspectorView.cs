#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Karin.DialogSystem.Tree
{
    [UxmlElement]
    public partial class InspectorView : VisualElement
    {
        private Editor editor;

        public InspectorView()
        {

        }

        public void UpdateSelection(NodeView view)
        {
            Clear();
            UnityEngine.Object.DestroyImmediate(editor);
            editor = Editor.CreateEditor(view.node);
            IMGUIContainer container = new IMGUIContainer(() => editor.OnInspectorGUI());
            container.style.marginBottom = 15;
            container.style.marginLeft = 15;
            container.style.marginRight = 15;
            container.style.marginTop = 15;
            Add(container);
        }
    }
}
#endif