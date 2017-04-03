using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR.WSA.Input;
using UnityEngine.Windows.Speech;

public class CursorClicked : MonoBehaviour
{
    private bool platformShowed = false;

    private GestureRecognizer recognizer;
    private DictationRecognizer dictationRecognizer;

    void OnSelect()
    {
        switch (GazeGestureManager.Instance.FocusedObject.tag)
        {
            case "MenuItem_Sticker":
                MainMenuManager.Instance.InputDialogState = InputDialogState.Sticker;
                MainMenuManager.Instance.SetInputDialogTitle("留言条");
                MainMenuManager.Instance.SetInputDialogContent("请说出留言条内容...");
                MainMenuManager.Instance.SetInputDialogVisibility(true);
                //startDictation();
                break;

            case "MenuItem_Platform":
                platformShowed = !platformShowed;
                PlatformManager.Instance.SetVisibility(platformShowed);
                break;

            case "MenuItem_Search":
                MainMenuManager.Instance.InputDialogState = InputDialogState.Search;
                MainMenuManager.Instance.SetInputDialogTitle("查找图书");
                MainMenuManager.Instance.SetInputDialogContent("请说出图书编号...");
                MainMenuManager.Instance.SetInputDialogVisibility(true);
                //startDictation();
                break;

            case "MenuItem_About":
                MainMenuManager.Instance.SetAboutDialogVisibility(true);
                break;

            case "CancelButton_About":
                MainMenuManager.Instance.SetAboutDialogVisibility(false);
                break;

            case "CancelButton_Input":
                MainMenuManager.Instance.SetInputDialogVisibility(false);
                //StopDictation();
                break;

            case "ConfirmButton_Input":
                MainMenuManager.Instance.SetInputDialogVisibility(false);
                //StopDictation();

                MainMenuManager.Instance.SetVisibility(false);

                MainScreenManager.Instance.SetScreenTip(ScreenTipContent.ShowMenu);

                if (MainMenuManager.Instance.InputDialogState == InputDialogState.Sticker)
                {
                    ImageTargetManager.Instance.SetStickerContent(MainMenuManager.Instance.GetInputDialogContent());

                    CursorManager.Instance.SetTipText("点击一下开始识别并粘贴");
                    CursorManager.Instance.TipBusy = true;

                    // Set up a GestureRecognizer to detect Select gestures.
                    recognizer = new GestureRecognizer();
                    recognizer.TappedEvent += (source, tapCount, ray) =>
                    {
                        CursorManager.Instance.TipBusy = false;
                        CursorManager.Instance.SetTipText("");

                        ImageTargetManager.Instance.BuildNewTarget();
                    };
                    recognizer.StartCapturingGestures();
                }
                else
                {
                    MainScreenManager.Instance.SetScreenTip(ScreenTipContent.HideNavigation);

                    string iid = MainMenuManager.Instance.GetInputDialogContent();
                    Debug.Log(iid);
                    string id = "TM001";

                    NavigationManager.Instance.StartNavigation(id);
                }
                
                break;

            case "Building1":
                PlatformManager.Instance.SetBuilding1StructureVisibility(true);
                break;

            case "Library":
                PlatformManager.Instance.SetLibraryStructureVisibility(true);
                break;

            case "Gym":
                break;

            case "Building1_Structure":
                PlatformManager.Instance.SetBuilding1StructureVisibility(false);
                break;

            case "Library_Structure":
                PlatformManager.Instance.SetLibraryStructureVisibility(false);
                break;

            default:
                break;
        }
    }

    private void startDictation()
    {
        PhraseRecognitionSystem.Shutdown();

        dictationRecognizer = new DictationRecognizer();
        //订阅事件  
        dictationRecognizer.DictationHypothesis += DictationRecognizer_DictationHypothesis;
        dictationRecognizer.DictationResult += DictationRecognizer_DictationResult;
        dictationRecognizer.DictationComplete += DictationRecognizer_DictationComplete;
        dictationRecognizer.DictationError += DictationRecognizer_DictationError;

        dictationRecognizer.Start();
    }

    private void DictationRecognizer_DictationError(string error, int hresult)
    {
        MainMenuManager.Instance.SetInputDialogContent(error);
    }

    private void DictationRecognizer_DictationComplete(DictationCompletionCause cause)
    {
        Debug.Log("complete");
        //如果在听写开始后第一个5秒内没听到任何声音，将会超时  
        //如果识别到了一个结果但是之后20秒没听到任何声音，也会超时  
        if (cause == DictationCompletionCause.TimeoutExceeded)
        {
            Debug.Log("Dictation has timed out.");
            dictationRecognizer.Stop();
            dictationRecognizer.Start();
        }
    }

    private void DictationRecognizer_DictationResult(string text, ConfidenceLevel confidence)
    {
        MainMenuManager.Instance.SetInputDialogContent(text);
    }

    private void DictationRecognizer_DictationHypothesis(string text)
    {
        MainMenuManager.Instance.SetInputDialogContent(text);
        Debug.Log("Hypothesis:" + text);
    }

    private void StopDictation()
    {
        dictationRecognizer.DictationResult -= DictationRecognizer_DictationResult;
        dictationRecognizer.DictationComplete -= DictationRecognizer_DictationComplete;
        dictationRecognizer.DictationHypothesis -= DictationRecognizer_DictationHypothesis;
        dictationRecognizer.DictationError -= DictationRecognizer_DictationError;
        dictationRecognizer.Dispose();

        PhraseRecognitionSystem.Restart();
    }
}
