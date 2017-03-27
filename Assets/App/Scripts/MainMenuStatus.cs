using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;
using UnityEngine.VR.WSA.Input;

public class MainMenuStatus : MonoBehaviour
{

    private GestureRecognizer recognizer;
    private DictationRecognizer dictationRecognizer;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            MainMenuManager.Instance.SetVisibility(true);
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            MainMenuManager.Instance.SetVisibility(false);
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            MainMenuManager.Instance.SetAboutDialogVisibility(true);
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            MainMenuManager.Instance.SetAboutDialogVisibility(false);
        }
        else if (Input.GetKeyDown(KeyCode.T))
        {
            CursorManager.Instance.SetTipText("");

            ImageTargetManager.Instance.BuildNewTarget();
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            PlatformManager.Instance.SetVisibility(true);
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            PlatformManager.Instance.SetVisibility(false);
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            MainMenuManager.Instance.SetInputDialogVisibility(true);
            startDictation();
        }
        else if (Input.GetKeyDown(KeyCode.V))
        {
            MainMenuManager.Instance.SetInputDialogVisibility(false);
            StopDictation();
        }
        else if (Input.GetKeyDown(KeyCode.M))
        {
            MainMenuManager.Instance.SetInputDialogVisibility(false);
            StopDictation();

            MainMenuManager.Instance.SetVisibility(false);

            ImageTargetManager.Instance.SetStickerContent(MainMenuManager.Instance.GetInputDialogContent());

            CursorManager.Instance.SetTipText("点击一下开始识别并粘贴");

            // Set up a GestureRecognizer to detect Select gestures.
            recognizer = new GestureRecognizer();
            recognizer.TappedEvent += (source, tapCount, ray) =>
            {
                CursorManager.Instance.SetTipText("");

                ImageTargetManager.Instance.BuildNewTarget();
            };
            recognizer.StartCapturingGestures();
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
