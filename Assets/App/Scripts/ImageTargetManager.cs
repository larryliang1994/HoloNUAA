using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageTargetManager : MonoBehaviour
{
    public static ImageTargetManager Instance { get; private set; }

    public GameObject userDefinedTargerBuilder;
    public GameObject stickerContentObject;

    private TextMesh stickerContentText;

    // Use this for initialization
    void Start ()
    {
        Instance = this;

        stickerContentText = stickerContentObject.GetComponent<TextMesh>();
    }
	
	public void BuildNewTarget()
    {
        UDTEventHandler handler = userDefinedTargerBuilder.GetComponent<UDTEventHandler>();
        handler.BuildNewTarget();
    }

    public void SetStickerContent(string text)
    {
        if (stickerContentText != null)
        {
            stickerContentText.text = text;
        }
    }

    public string GetStickerContent()
    {
        if (stickerContentText != null)
        {
            return stickerContentText.text;
        }
        else
        {
            return "";
        }
    }
}
