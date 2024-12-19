using System;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.UIElements;

namespace Karin.DialogSystem.Tree
{

    public class DialogEditor : EditorWindow
    {
        [SerializeField]
        private VisualTreeAsset m_VisualTreeAsset = default;

        private DialogView _dialogView;
        private InspectorView _inspectorView;
        private VisualElement _blockingScreen;

        private IMGUIContainer _blackboardView;

        private SerializedObject _treeObject;
        private SerializedProperty _blackboardProperty;

        [MenuItem("Tools/DialogTree")]
        public static void OpenWindow()
        {
            DialogEditor wnd = GetWindow<DialogEditor>();
            wnd.titleContent = new GUIContent("DialogVisual");
        }

        [OnOpenAsset]
        public static bool OnOpenAssets(int instanceID, int line)
        {
            if (Selection.activeObject is DialogTree)
            {
                OpenWindow();
                return true;
            }
            return false;
        }

        public void CreateGUI()
        {
            VisualElement root = rootVisualElement;

            var template = m_VisualTreeAsset.Instantiate();
            template.style.flexGrow = 1;
            root.Add(template);

            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/DialogSystem/DialogTree/Editor/DialogVisual.uss");
            root.styleSheets.Add(styleSheet);

            _dialogView = root.Q<DialogView>();
            _inspectorView = root.Q<InspectorView>();
            _blockingScreen = root.Q<VisualElement>("BlockingScreen");

            _dialogView.OnNodeSelected += OnSelectionNodeChanged;
            OnSelectionChange();

            _blackboardView = root.Q<IMGUIContainer>("Blackboard-IMGUI");
            _blackboardView.onGUIHandler = () =>
            {
                if (_treeObject != null && _treeObject.targetObject != null)
                {
                    _treeObject.Update();

                    EditorGUILayout.PropertyField(_blackboardProperty);
                    _treeObject.ApplyModifiedProperties();
                }

            };

        }

        private void OnSelectionNodeChanged(NodeView view)
        {
            _inspectorView.UpdateSelection(view);
        }

        private void OnSelectionChange()
        {
            DialogTree tree = Selection.activeObject as DialogTree;

            if (tree != null && AssetDatabase.CanOpenAssetInEditor(tree.GetInstanceID()))
            {
                _treeObject = new SerializedObject(tree);
                _blackboardProperty = _treeObject.FindProperty("blackboard");
                _dialogView.PopulateView(tree);
                _blockingScreen.style.top = new Length(100, LengthUnit.Percent);
            }
            else
                _blockingScreen.style.top = new Length(0, LengthUnit.Percent);
        }
    }

}