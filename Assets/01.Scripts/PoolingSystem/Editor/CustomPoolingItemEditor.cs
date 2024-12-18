using Karin.PoolingSystem;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PoolItemSO))]
public class CustomPoolingItemEditor : Editor
{
    private SerializedProperty enumNameProp;
    private SerializedProperty poolingNameProp;
    private SerializedProperty poolCountProp;
    private SerializedProperty prefabProp;


    private GUIStyle textAreaStyle;

    private void OnEnable()
    {
        GUIUtility.keyboardControl = 0;

        enumNameProp = serializedObject.FindProperty("typeName");
        poolingNameProp = serializedObject.FindProperty("name");
        poolCountProp = serializedObject.FindProperty("count");
        prefabProp = serializedObject.FindProperty("prefab");
    }

    private void StyleSetup()
    {
        if (textAreaStyle == null)
        {
            textAreaStyle = new GUIStyle(EditorStyles.textArea);
            textAreaStyle.wordWrap = true;
        }
    }

    public override void OnInspectorGUI()
    {
        StyleSetup();

        serializedObject.Update();

        EditorGUILayout.BeginHorizontal("HelpBox");
        {
            EditorGUILayout.BeginVertical();
            {

                EditorGUI.BeginChangeCheck(); //������ üũ�Ѵ�.
                string prevName = enumNameProp.stringValue;
                //���Ͱ� �����ų� ��Ŀ���� ���������� ������ �������� �ʾ�.
                EditorGUILayout.DelayedTextField(enumNameProp);

                if (EditorGUI.EndChangeCheck())
                {
                    //���� �������� ������ ��θ� �˾Ƴ���.
                    string assetPath = AssetDatabase.GetAssetPath(target);
                    string newName = $"Pool_{enumNameProp.stringValue}";
                    serializedObject.ApplyModifiedProperties();

                    string msg = AssetDatabase.RenameAsset(assetPath, newName);

                    //���������� ���ϸ� �����߾��.
                    if (string.IsNullOrEmpty(msg))
                    {
                        target.name = newName;
                        EditorGUILayout.EndVertical();
                        EditorGUILayout.EndHorizontal();
                        return;
                    }
                    enumNameProp.stringValue = prevName;
                    ((PoolItemSO)target).type = (PoolingType)System.Enum.Parse(typeof(PoolingType), prevName);
                }


                EditorGUILayout.PropertyField(poolingNameProp);


                EditorGUILayout.BeginHorizontal();
                {
                    EditorGUILayout.PrefixLabel("PoolSettings");
                    EditorGUILayout.PropertyField(poolCountProp, GUIContent.none);
                    EditorGUILayout.PropertyField(prefabProp, GUIContent.none);
                }
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndVertical();
        }
        EditorGUILayout.EndHorizontal();


        serializedObject.ApplyModifiedProperties();
    }
}
