using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainScreenManager : MonoBehaviour
{
    public static MainScreenManager Instance { get; private set; }
    public static bool DotRunning = true;

    public Text menuTipObject;
    public Text locationTipObject;

    private IEnumerator coroutine;

    // Use this for initialization
    void Start ()
    {
        Instance = this;

        coroutine = LocationDot(1.0f);
        StartCoroutine(coroutine);
    }

    private IEnumerator LocationDot(float waitTime)
    {
        int dot = 1;

        while (true)
        {
            string locationDot = "";

            if (dot % 3 == 1)
            {
                locationDot = "。";
            }
            else if (dot % 3 == 2)
            {
                locationDot = "。。";
            }
            else if (dot % 3 == 0)
            {
                locationDot = "。。。";
            }

            dot++;

            SetLocationTip(locationDot);

            yield return new WaitForSeconds(waitTime);
        }
    }

    public void SetScreenTip(ScreenTipContent content)
    {
        if (menuTipObject != null)
        {
            switch (content)
            {
                case ScreenTipContent.ShowMenu:
                    menuTipObject.text = "说“show menu”以显示主菜单";
                    break;

                case ScreenTipContent.HideMenu:
                    menuTipObject.text = "说“hide menu”以隐藏主菜单";
                    break;

                case ScreenTipContent.HideNavigation:
                    menuTipObject.text = "说“hide navigation”以隐藏导航";
                    break;
            }
        }
    }

    public void SetLocationTip(string text)
    {
        if (locationTipObject != null)
        {
            locationTipObject.text = text;
        }
    }
}

public enum ScreenTipContent
{
    ShowMenu = 0,
    HideMenu,
    HideNavigation
}
