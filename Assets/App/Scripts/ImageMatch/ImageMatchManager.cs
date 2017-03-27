using UnityEngine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
//using System.Drawing;
using System.Linq;
using System.Text;

/*
using Emgu.CV;
using Emgu.CV.Util;
using Emgu.Util;
using Emgu.CV.Structure;
using Emgu.CV.Features2D;
*/
public class ImageMatchManager : MonoBehaviour {
    /*
    [DllImport("ImageMatch")]
    private static extern void AlgorithmTest(string img1, string img2, int method);

    [DllImport("ImageMatch")]
    private static extern void FullTest();

    [DllImport("ImageMatch")]
    private static extern void WriteDescriptors(int method);

    [DllImport("ImageMatch")]
    private static extern void ReadDescriptors();

    [DllImport("ImageMatch")]
    private static extern void MatchAnImage(string img, int method, StringBuilder name, StringBuilder tag);

    [DllImport("ImageMatch")]
    private static extern void ConnectionCheck(StringBuilder input, ref int num);
    */
    public static ImageMatchManager Instance { get; private set; }
    
    // Use this for initialization
    void Start ()
    {
        Instance = this;

        string img1 = Application.dataPath + @"/App/Images/img1.jpg";
        string img2 = Application.dataPath + @"/App/Images/img2.jpg";
        //match(img1, img2);
    }

    public void StartMatching(string path)
    {
        Debug.Log("OnStartMatching");

        Debug.Log("path");

        //System.Threading.Thread.Sleep(2000);

        CursorManager.Instance.SetTipText("识别完成，这个是图书馆");
    }
	
	
    /*
    private void match(string imageName1, string imageName2)
    {
        Image<Bgr, Byte> trackimage_rgb;
        Image<Gray, Byte> temp, img1, img2, trackimage;
        VectorOfKeyPoint vok1, vok2;
        Mat descriptors_1, descriptors_2, result_image;
        MKeyPoint[] keypoints1, keypoints2;
        
        img1 = new Image<Gray, Byte>(imageName1);
        
        ORBDetector od = new ORBDetector(500, 1.2f, 8, 31, 0, 2, ORBDetector.ScoreType.Fast, 31, 20);
        keypoints1 = od.Detect(img1);
        vok1 = new VectorOfKeyPoint(keypoints1);
        descriptors_1 = new Mat();
        od.Compute(img1, vok1, descriptors_1);

        img2 = new Image<Gray, Byte>(imageName2);
        od = new ORBDetector(500, 1.2f, 8, 31, 0, 2, ORBDetector.ScoreType.Fast, 31, 20);
        keypoints2 = od.Detect(img2);
        vok2 = new VectorOfKeyPoint(keypoints2);
        descriptors_2 = new Mat();
        od.Compute(img2, vok2, descriptors_2);

        Debug.Log("d1 size = " + descriptors_1.Size);
        Debug.Log("d2 size = " + descriptors_2.Size);

        float minRatio = 0.8f;
        float maxDistance = 50f;
        LineSegment2DF line;
        trackimage = img1.Copy();
        trackimage_rgb = new Image<Bgr, Byte>(imageName1);
        BFMatcher matcher = new BFMatcher(DistanceType.Hamming);
        matcher.Add(descriptors_1);
        VectorOfDMatch matches = new VectorOfDMatch();
        VectorOfVectorOfDMatch knnmatches = new VectorOfVectorOfDMatch();
        matcher.KnnMatch(descriptors_2, knnmatches, 2, null);
        MDMatch[] bestMatch = new MDMatch[1];
        MDMatch betterMatch;
        for (int i = 0; i < knnmatches.Size; i++)
        {
            bestMatch[0] = knnmatches[i][0];
            betterMatch = knnmatches[i][1];
            line = new LineSegment2DF(keypoints1[bestMatch[0].TrainIdx].Point, keypoints2[bestMatch[0].QueryIdx].Point);
            float distanceRatio = bestMatch[0].Distance / betterMatch.Distance;
            if (distanceRatio < minRatio && line.Length < maxDistance)
            {
                matches.Push(bestMatch);
                trackimage.Draw(line, new Gray(255), 2);
                trackimage_rgb.Draw(line, new Bgr(0, 255, 0), 2);
            }
        }
        VectorOfVectorOfDMatch goodMatches = new VectorOfVectorOfDMatch(matches);
        result_image = new Mat();
        MCvScalar matchcolor = new MCvScalar(0, 255, 0);
        MCvScalar singlepointcolor = new MCvScalar(0, 0, 255);
        Features2DToolbox.DrawMatches(img1, vok1, img2, vok2, goodMatches, result_image, matchcolor, singlepointcolor);

        Debug.Log("size = " + goodMatches.Size);
    }
    */
}
