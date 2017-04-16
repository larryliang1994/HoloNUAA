using System.Linq;
using UnityEngine;
using UnityEngine.VR.WSA.WebCam;
using UnityEngine.VR.WSA.Input;
using UnityEngine.UI;

public class CaptureManager : MonoBehaviour {

    public static CaptureManager Instance { get; private set; }

    private PhotoCapture photoCaptureObject = null;
    private GestureRecognizer recognizer;
    
    private string filePath;

    private void Start()
    {
        Instance = this;
    }

    public void Capture()
    {
        PhotoCapture.CreateAsync(false, OnPhotoCaptureCreated);
    }

    void OnPhotoCaptureCreated(PhotoCapture captureObject)
    {
        Debug.Log("OnPhotoCaptureCreated");

        photoCaptureObject = captureObject;

        Resolution cameraResolution = PhotoCapture.SupportedResolutions.OrderByDescending((res) => res.width * res.height).First();

        CameraParameters c = new CameraParameters();
        c.hologramOpacity = 0.0f;
        c.cameraResolutionWidth = cameraResolution.width;
        c.cameraResolutionHeight = cameraResolution.height;
        c.pixelFormat = CapturePixelFormat.BGRA32;

        captureObject.StartPhotoModeAsync(c, OnPhotoModeStarted);

        Debug.Log("OnPhotoCaptureCreated Successfully");
    }

    private void OnPhotoModeStarted(PhotoCapture.PhotoCaptureResult result)
    {
        if (result.success)
        {
            Debug.Log("Capture Successfully");

            //photoCaptureObject.TakePhotoAsync(OnCapturedPhotoToMemory);

            string filename = string.Format(@"CapturedImage{0}_n.jpg", Time.time);
            filePath = System.IO.Path.Combine(Application.persistentDataPath, filename);

            photoCaptureObject.TakePhotoAsync(filePath, PhotoCaptureFileOutputFormat.JPG, OnCapturedPhotoToDisk);
        }
        else
        {
            Debug.Log("Unable to start photo mode!");
        }
    }

    void OnCapturedPhotoToMemory(PhotoCapture.PhotoCaptureResult result, PhotoCaptureFrame photoCaptureFrame)
    {
        if (result.success)
        {
            // Create our Texture2D for use and set the correct resolution
            Resolution cameraResolution = PhotoCapture.SupportedResolutions.OrderByDescending((res) => res.width * res.height).First();
            Texture2D targetTexture = new Texture2D(cameraResolution.width, cameraResolution.height);
            // Copy the raw image data into our target texture
            photoCaptureFrame.UploadImageDataToTexture(targetTexture);
            // Do as we wish with the texture such as apply it to a material, etc.

            Debug.Log("To Memory Successfully!");
        }
        // Clean up
        photoCaptureObject.StopPhotoModeAsync(OnStoppedPhotoMode);
    }

    void OnCapturedPhotoToDisk(PhotoCapture.PhotoCaptureResult result)
    {
        if (result.success)
        {
            Debug.Log("Saved Photo to disk!");
            photoCaptureObject.StopPhotoModeAsync(OnStoppedPhotoMode);

            ImageMatchManager.Instance.Match(filePath);
        }
        else
        {
            Debug.Log("Failed to save Photo to disk");
        }
    }

    void OnStoppedPhotoMode(PhotoCapture.PhotoCaptureResult result)
    {
        Debug.Log("OnStoppedPhotoMode");
        photoCaptureObject.Dispose();
        photoCaptureObject = null;
    }
}
