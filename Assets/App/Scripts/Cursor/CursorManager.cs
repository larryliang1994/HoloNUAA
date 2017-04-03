using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public static CursorManager Instance { get; private set; }

    public GameObject tipObject;

    public bool TipBusy = false;
    private TextMesh tipText;

    // Use this for initialization
    private void Start ()
    {
        Instance = this;

        tipText = tipObject.GetComponent<TextMesh>();
        
    }

    public string GetTipText()
    {
        return tipText.text;
    }

    public void SetTipText(string text)
    {
        if (tipText != null && !TipBusy)
        {
            tipText.text = text;
        }
    }
}
