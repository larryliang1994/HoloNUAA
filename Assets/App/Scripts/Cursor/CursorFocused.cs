using UnityEngine;
//using ImageMatchWRT;

public class CursorFocused : MonoBehaviour
{
    private static string oldFocuedTag = "";

    private void Update()
    {
        if (GazeGestureManager.Instance.FocusedObject == null)
        {
            CursorManager.Instance.SetTipText("");

            if (oldFocuedTag != "")
            {
                SetMeshRenderer(oldFocuedTag, false);
            }

            return;
        }

        string focusedTag = "";
        switch (GazeGestureManager.Instance.FocusedObject.tag)
        {
            case "MenuItem_Move":   focusedTag = "MenuItem_Move_Back";  break;

            case "MenuItem_Rotate": focusedTag = "MenuItem_Rotate_Back";    break;

            case "MenuItem_Scale":  focusedTag = "MenuItem_Scale_Back"; break;

            case "MenuItem_Restore":    focusedTag = "MenuItem_Restore_Back";   break;

            case "MenuItem_Remove": focusedTag = "MenuItem_Remove_Back";    break;

            case "MenuItem_Location": focusedTag = "MenuItem_Location_Back"; break;

            case "MenuItem_Sticker": focusedTag = "MenuItem_Sticker_Back"; break;

            case "MenuItem_Platform": focusedTag = "MenuItem_Platform_Back"; break;

            case "MenuItem_Others": focusedTag = "MenuItem_Others_Back"; break;

            case "MenuItem_Search": focusedTag = "MenuItem_Search_Back"; break;

            case "MenuItem_About": focusedTag = "MenuItem_About_Back"; break;

            case "CancelButton_About":  focusedTag = "CancelButton_About_Back"; break;

            case "CancelButton_Input": focusedTag = "CancelButton_Input_Back"; break;

            case "ConfirmButton_Input": focusedTag = "ConfirmButton_Input_Back";    break;

            case "Building1":
                CursorManager.Instance.SetTipText("南航主楼");
                return;

            case "Library":
                CursorManager.Instance.SetTipText("图书馆");
                return;

            case "Gym":
                CursorManager.Instance.SetTipText("体育馆");
                return;

            case "Building1_Structure":
            case "Library_Structure":
                CursorManager.Instance.SetTipText("点击关闭");
                return;

            default:
                CursorManager.Instance.SetTipText("");

                if (oldFocuedTag != "")
                {
                    SetMeshRenderer(oldFocuedTag, false);
                    oldFocuedTag = "";
                }
                return;
        }

        CursorManager.Instance.SetTipText("");

        if (focusedTag == oldFocuedTag)
        {
            return;
        }

        SetMeshRenderer(focusedTag, true);
        
        if (oldFocuedTag != "")
        {
            SetMeshRenderer(oldFocuedTag, false);
        }

        oldFocuedTag = focusedTag;
    } 

    private void SetMeshRenderer(string tag, bool enable)
    {
        GameObject parent = GameObject.FindGameObjectWithTag(tag);

        if (parent == null)
        {
            return;
        }

        MeshRenderer[] children = parent.GetComponentsInChildren<MeshRenderer>(true);

        foreach (MeshRenderer child in children)
        {
            child.enabled = enable;
        }
    }
}
