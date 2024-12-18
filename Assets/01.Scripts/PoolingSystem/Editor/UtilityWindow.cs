using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Karin.PoolingSystem;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

public enum UtilType
{
    Pool
}

public class UtilityWindow : EditorWindow
{
    private static int toolbarIndex = 0;
    private static Dictionary<UtilType, Vector2> scrollPositions
        = new Dictionary<UtilType, Vector2>();
    private static Dictionary<UtilType, Object> selectedItem
        = new Dictionary<UtilType, Object>();
    
    private static Vector2 inspectorScroll = Vector2.zero;

    private string[] _toolbarItemNames;
    private Editor _cachedEditor;
    private Texture2D _selectTexture;
    private GUIStyle _selectStyle;


    #region �� ������ ���̺� ����
    private readonly string _poolDirectory = "Assets/06.SO/ObjectPool";
    private PoolingListSO _poolTable;
    #endregion

    [MenuItem("Tools/Utility")]
    private static void OpenWindow()
    {
        UtilityWindow window = GetWindow<UtilityWindow>("Pool");
        window.minSize = new Vector2(700, 500);
        window.Show();
    }

    private void OnEnable()
    {
        SetUpUtility();
    }

    private void OnDisable()
    {
        DestroyImmediate(_cachedEditor);
        DestroyImmediate(_selectTexture);
    }

    //��ƿ��Ƽ ���� �¾��ϴ� �Լ�.
    private void SetUpUtility()
    {
        _selectTexture = new Texture2D(1, 1); //1�ȼ�¥�� �ؽ��� �׸�
        _selectTexture.SetPixel(0, 0, new Color(0.31f, 0.40f, 0.50f));
        _selectTexture.Apply();

        _selectStyle = new GUIStyle();
        _selectStyle.normal.background = _selectTexture;
        
        _selectTexture.hideFlags = HideFlags.DontSave;

        _toolbarItemNames = Enum.GetNames(typeof(UtilType));
        
        foreach(UtilType type in Enum.GetValues(typeof(UtilType)))
        {
            if(scrollPositions.ContainsKey(type) == false)
                scrollPositions[type] = Vector2.zero;

            if (selectedItem.ContainsKey(type) == false)
                selectedItem[type] = null;
        }

        if(_poolTable == null)
        {
            _poolTable = CreateAssetTable<PoolingListSO>(_poolDirectory);
        }
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    private T CreateAssetTable<T>(string path) where T : ScriptableObject
    {
        T table = AssetDatabase.LoadAssetAtPath<T>($"{path}/table.asset");
        if (table == null)
        {
            table = ScriptableObject.CreateInstance<T>();

            string fileName = AssetDatabase.GenerateUniqueAssetPath($"{path}/table.asset");
            AssetDatabase.CreateAsset(table, fileName);
            Debug.Log($"{typeof(T).Name} Table Created At : {fileName}");
        }
        return table;
    }

    private void OnGUI()
    {
        toolbarIndex = GUILayout.Toolbar(toolbarIndex, _toolbarItemNames);
        EditorGUILayout.Space(5f);

        DrawContent();
    }

    private void DrawContent()
    {
        DrawPoolItems();
    }

    

    private void DrawPoolItems()
    {
        //��ܿ� �޴� 2���� ������.
        EditorGUILayout.BeginHorizontal();
        {
            GUI.color = new Color(0.19f, 0.76f, 0.08f);
            if(GUILayout.Button("Generate Item"))
            {
                GeneratePoolItem();
            }

            GUI.color = new Color(0.81f, 0.13f, 0.18f);
            if(GUILayout.Button("Generate enum file"))
            {
                GenerateEnumFile();
            }
        }
        EditorGUILayout.EndHorizontal();

        GUI.color = Color.white; //���� �������� ����.

        EditorGUILayout.BeginHorizontal();
        {

            //���� Ǯ����Ʈ ��ºκ�
            EditorGUILayout.BeginVertical(EditorStyles.helpBox, GUILayout.Width(300f));
            {
                EditorGUILayout.LabelField("Pooling list");
                EditorGUILayout.Space(3f);


                scrollPositions[UtilType.Pool] = EditorGUILayout.BeginScrollView(
                    scrollPositions[UtilType.Pool], 
                    false, true, GUIStyle.none, GUI.skin.verticalScrollbar, GUIStyle.none);
                {

                    foreach(var item in _poolTable.list)
                    {
                        GUIStyle style = selectedItem[UtilType.Pool] == item ?
                                                _selectStyle : GUIStyle.none;

                        EditorGUILayout.BeginHorizontal(style, GUILayout.Height(40f));
                        {
                            EditorGUILayout.LabelField(item.name, GUILayout.Height(40f), GUILayout.Width(240f));

                            EditorGUILayout.BeginVertical();
                            {
                                EditorGUILayout.Space(10f);
                                GUI.color = Color.red;
                                if (GUILayout.Button("X", GUILayout.Width(20f)))
                                {
                                    //_poolTable.datas ���⼭ �ش��ϴ� �༮�� �����ؾ���
                                    _poolTable.list.Remove(item);
                                    //Assetdatabase.DeleteAsset����� �̿��ؼ� ������ SO�� �����ؾ���
                                    AssetDatabase.DeleteAsset( AssetDatabase.GetAssetPath(item));
                                    // _poolTable �����ٰ� �̾߱������ ��
                                    EditorUtility.SetDirty(_poolTable);
                                    // SaveAsset�� ���ؼ� �������ָ� ��.
                                    AssetDatabase.SaveAssets();
                                }
                                GUI.color = Color.white;
                            }
                            EditorGUILayout.EndVertical();
                        }
                        EditorGUILayout.EndHorizontal();

                        //���������� �׸� �簢�� ������ �˾ƿ´�.
                        Rect lastRect = GUILayoutUtility.GetLastRect();

                        if(Event.current.type == EventType.MouseDown 
                            && lastRect.Contains(Event.current.mousePosition))
                        {
                            inspectorScroll = Vector2.zero;
                            selectedItem[UtilType.Pool] = item;
                            Event.current.Use();
                        }

                        //�����Ȱ� Ȯ���ϸ� break�� �ɾ��ָ� ��.
                        if (item == null)
                            break;

                    } 
                    //end of foreach

                }
                EditorGUILayout.EndScrollView();   

            }
            EditorGUILayout.EndVertical();

            //�ν����͸� �׷���� ��.
            if (selectedItem[UtilType.Pool] != null)
            {
                inspectorScroll = EditorGUILayout.BeginScrollView(inspectorScroll);
                {
                    EditorGUILayout.Space(2f);
                    Editor.CreateCachedEditor(
                        selectedItem[UtilType.Pool], null, ref _cachedEditor);

                    _cachedEditor.OnInspectorGUI();
                }
                EditorGUILayout.EndScrollView();
            }
        }
        EditorGUILayout.EndHorizontal();

    }


    private void GeneratePoolItem()
    {
        Guid guid = Guid.NewGuid(); //������ ���ڿ� Ű�� ��ȯ��
        
        PoolItemSO item = CreateInstance<PoolItemSO>(); //�̰� �޸𸮿��� �����Ѱž�.
        item.name = guid.ToString();

        //������ �������� �����߰� ����Ʈ�� �����߾�.
        AssetDatabase.CreateAsset(item, $"{_poolDirectory}/Pool_{item.name}.asset");
        _poolTable.list.Add(item);

        EditorUtility.SetDirty(_poolTable);  //�� ���̺� ������ �Ͼ���� �˷���� ��
        AssetDatabase.SaveAssets(); //����� �͵��� �ν��ؼ� ������ �Ѵ�.
    }

    private void GenerateEnumFile()
    {
        StringBuilder codeBuilder = new StringBuilder();

        foreach(var item in _poolTable.list)
        {
            codeBuilder.Append(item.typeName);
            codeBuilder.Append(",");
        }

        string code = string.Format(CodeFormat.PoolingTypeFormat, codeBuilder.ToString());

        string path = $"{Application.dataPath}/01.Scripts/PoolingSystem/PoolingType.cs";

        File.WriteAllText(path, code);
        AssetDatabase.Refresh(); //�ٽ� ������ ����
    }

}
