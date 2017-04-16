using UnityEngine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using UnityEngine.UI;
using System.Collections;
using Vuforia;

public class ImageMatchManager : MonoBehaviour
{
    public static ImageMatchManager Instance { get; private set; }
    public static float MatchRate;
    public static StringBuilder Name;
    public static StringBuilder Tag;

    public GameObject LibraryTarget;
    public GameObject Building1Target;
    public GameObject GymnasiumTarget;
    public GameObject Building2Target;

    private bool ShouldStop = false;

    [DllImport("ImageMatchDll")]
    private static extern int Add(int a, int b);
    
    [DllImport("ImageMatchDll")]
    private static extern void ConnectionCheck(StringBuilder input, ref int num);

    [DllImport("ImageMatchDll")]
    private static extern void WriteDescriptors(string source_path, string des_path, int method, int param);

    [DllImport("ImageMatchDll")]
    private static extern void MiniReadDescriptors(string des_path);

    [DllImport("ImageMatchDll")]
    private static extern void ReadDescriptors(string des_path);

    [DllImport("ImageMatchDll")]
    private static extern void InitData(string source_path, string des_path, int method, int param);

    [DllImport("ImageMatchDll")]
    private static extern void MatchAnImage(string img, int method, StringBuilder name, StringBuilder tag);

    [DllImport("ImageMatchDll")]
    private static extern void AlgorithmTest(string img1, string img2, int method);

    [DllImport("ImageMatchDll")]
    private static extern void FullTest(string source_path, string des_path, string test_path, int method);

    [DllImport("ImageMatchDll")]
    private static extern void MiniInitData(string source_path, string des_path, int method, int param);

    [DllImport("ImageMatchDll")]
    private static extern void MiniAlgorithmTest(string img1, string img2, int method);

    [DllImport("ImageMatchDll")]
    private static extern void MiniFullTest(string source_path, string des_path, string test_path, int method, int param);

    [DllImport("ImageMatchDll")]
    private static extern void StdInitData(string mini_path, string source_path, string minides_path, string des_path, int method, int miniParam, int param);

    [DllImport("ImageMatchDll")]
    private static extern void StdFullTest(string mini_path, string source_path, string minides_path, string des_path, string test_path, int method, int miniParam, int param);

    [DllImport("ImageMatchDll")]
    private static extern float StdMatchAnImage(string img, int method, StringBuilder name, StringBuilder tag, int miniParam, int param);

    private void Start()
    {
        Instance = this;

        Name = new StringBuilder();
        Tag = new StringBuilder();
    }

    public void InitData()
    {
        string mini_path = Application.dataPath + @"/App/Images/imgs_mini/";
        string source_path = Application.dataPath + @"/App/Images/imgs_source/";
        string minides_path = Application.dataPath + @"/App/des_mini/";
        string des_path = Application.dataPath + @"/App/des/";
        int method = 1;
        int miniParam = 300;
        int param = 1000;

        try
        {
            StdInitData(mini_path, source_path, minides_path, des_path, method, miniParam, param);
        }
        catch (DllNotFoundException exception)
        {
            Debug.Log(exception);
        }
    }

    public void StartMatching()
    {
        if (!ShouldStop)
        {
            CaptureManager.Instance.Capture();
        }
    }

    public void Match(string path)
    {
        int method = 1;
        int miniParam = 300;
        int param = 1000;

        try
        {
            MatchRate = StdMatchAnImage(path, method, Name, Tag, miniParam, param);

            if (MatchRate > 0.1)
            {
                GameObject currentTarget = null;

                switch (Tag.ToString())
                {
                    case "Library":
                        currentTarget = LibraryTarget;
                        break;

                    case "Building1":
                        currentTarget = Building1Target;
                        break;

                    case "Gymnasium":
                        currentTarget = GymnasiumTarget;
                        break;

                    case "Building2":
                        currentTarget = Building2Target;
                        break;
                }

                currentTarget.GetComponent<ImageTargetTrackableEventHandler>().OnTrackableStateChanged(TrackableBehaviour.Status.NOT_FOUND, TrackableBehaviour.Status.DETECTED);
            }
            else
            {
                StartMatching();
            }
        }
        catch (DllNotFoundException exception)
        {
            Debug.Log(exception);
        }
    }

    public void StopMatching()
    {
        ShouldStop = true;
    }
}
