using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager Instance { get; private set; }

    public GameObject aboutObject;
    public GameObject inputObject;
    public GameObject inputDialogContentObject;
    public GameObject inputDialogTitleObject;

    public InputDialogState InputDialogState;

    private TextMesh inputDialogContentText;
    private TextMesh inputDialogTitleText;

    // Use this for initialization
    void Start ()
    {
        Instance = this;

        inputDialogContentText = inputDialogContentObject.GetComponent<TextMesh>();
        inputDialogTitleText = inputDialogTitleObject.GetComponent<TextMesh>();
    }

    public void SetVisibility(bool visible)
    {
        Animator anim = this.GetComponent<Animator>();
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);

        if (visible && (stateInfo.IsName("Base Layer.MainMenuDisappear")))
        {
            this.transform.position = Camera.main.transform.position + Camera.main.transform.forward.normalized * 2.0f;

            anim.SetTrigger("appear");

            MainScreenManager.Instance.SetScreenTip(ScreenTipContent.HideMenu);
        }
        else if (!visible && (stateInfo.IsName("Base Layer.MainMenuAppear") || stateInfo.IsName("Base Layer.InitState")))
        {
            anim.SetTrigger("disappear");

            MainScreenManager.Instance.SetScreenTip(ScreenTipContent.ShowMenu);
        }
    }

    public void SetInputDialogVisibility(bool visible)
    {
        Animator inputAnim = inputObject.GetComponent<Animator>();
        AnimatorStateInfo inputStateInfo = inputAnim.GetCurrentAnimatorStateInfo(0);

        if (visible && (inputStateInfo.IsName("Base Layer.InputDisappear") || inputStateInfo.IsName("Base Layer.InitState")))
        {
            inputAnim.SetTrigger("appear");

            SetInputDialogContent("请说出内容...");
        }
        else if (!visible && inputStateInfo.IsName("Base Layer.InputAppear"))
        {
            inputAnim.SetTrigger("disappear");
        }
    }

    public void SetAboutDialogVisibility(bool visible)
    {
        Animator aboutAnim = aboutObject.GetComponent<Animator>();
        AnimatorStateInfo aboutStateInfo = aboutAnim.GetCurrentAnimatorStateInfo(0);

        if (visible && (aboutStateInfo.IsName("Base Layer.AboutDisappear") || aboutStateInfo.IsName("Base Layer.InitState")))
        {
            aboutAnim.SetTrigger("appear");
        }
        else if (!visible && aboutStateInfo.IsName("Base Layer.AboutAppear"))
        {
            aboutAnim.SetTrigger("disappear");
        }
    }

    public void SetInputDialogContent(string text)
    {
        if (inputDialogContentText != null)
        {
            inputDialogContentText.text = text;
        }
    }

    public string GetInputDialogContent()
    {
        if (inputDialogContentText != null)
        {
            return inputDialogContentText.text;
        }
        else
        {
            return "";
        }
    }

    public void SetInputDialogTitle(string text)
    {
        if (inputDialogTitleText != null)
        {
            inputDialogTitleText.text = text;
        }
    }

    public string GetInputDialogTitle()
    {
        if (inputDialogTitleText != null)
        {
            return inputDialogTitleText.text;
        }
        else
        {
            return "";
        }
    }
}

public enum InputDialogState
{
    Sticker = 0,
    Search
}
