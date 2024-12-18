using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public class HierarchyDragAndDropHandler
{
    static HierarchyDragAndDropHandler()
    {
        EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyGUI;
    }

    private static void OnHierarchyGUI(int instanceID, Rect selectionRect)
    {
        Event currentEvent = Event.current;

        // 드래그 업데이트
        if (currentEvent.type == EventType.DragUpdated && DragAndDrop.objectReferences.Length > 0)
        {
            if (IsValidScriptDrag(DragAndDrop.objectReferences[0]))
            {
                DragAndDrop.visualMode = DragAndDropVisualMode.Copy; // 드래그 가능 표시
                currentEvent.Use();
            }
        }

        // 드래그 드롭 처리
        if (currentEvent.type == EventType.DragPerform && DragAndDrop.objectReferences.Length > 0)
        {
            if (IsValidScriptDrag(DragAndDrop.objectReferences[0]))
            {
                DragAndDrop.AcceptDrag();
                foreach (Object draggedObject in DragAndDrop.objectReferences)
                {
                    if (draggedObject is MonoScript script)
                    {
                        // 스크립트로부터 컴포넌트 타입 가져오기
                        System.Type scriptType = script.GetClass();
                        if (scriptType != null && scriptType.IsSubclassOf(typeof(MonoBehaviour)))
                        {
                            // 새로운 게임 오브젝트 생성
                            GameObject newObject = new GameObject(scriptType.Name);
                            Undo.RegisterCreatedObjectUndo(newObject, "Create Object with Script");

                            // 스크립트 추가
                            newObject.AddComponent(scriptType);

                            // 하이어라키 선택
                            Selection.activeObject = newObject;
                        }
                    }
                }
                currentEvent.Use();
            }
        }
    }

    private static bool IsValidScriptDrag(Object obj)
    {
        if (obj is MonoScript script)
        {
            // 스크립트가 MonoBehaviour인지 확인
            System.Type scriptType = script.GetClass();
            return scriptType != null && scriptType.IsSubclassOf(typeof(MonoBehaviour));
        }
        return false;
    }
}
