using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity;

public class PlatformManager : MonoBehaviour
{
    public static PlatformManager Instance { get; private set; }

    public GameObject building1;
    public GameObject library;
    public GameObject gym;
    public GameObject building1Structure;
    public GameObject libraryStructure;
    public GameObject charactor;

    public GameObject platformIndicator;
    public GameObject building1StructureIndicator;
    public GameObject libraryStructureIndicator;

    private IEnumerator coroutine;

    public static UserPosition UserCurrentPosition;

    // Use this for initialization
    void Start ()
    {
        Instance = this;

        SetTag();

        SetVisibility(false);
    }

    public void SetVisibility(bool visible)
    {
        //platformIndicator.GetComponent<DirectionIndicator>().Active = visible;

        this.transform.position = Camera.main.transform.position + Camera.main.transform.forward.normalized * 1.5f
            + new Vector3(0, -0.5f, 0);

        Vector3 directionToTarget = Camera.main.transform.position - this.transform.position;

        directionToTarget.y = 0.0f;
        
        // If we are right next to the camera the rotation is undefined. 
        if (directionToTarget.sqrMagnitude >= 0.001f)
        {
            // Calculate and apply the rotation required to reorient the object
            this.transform.rotation = Quaternion.LookRotation(-directionToTarget);
        }

        SetUserPosition();

        MeshRenderer[] marr = this.GetComponentsInChildren<MeshRenderer>(true);
        foreach (MeshRenderer m in marr)
        {
            m.enabled = visible;
        }

        SetBuilding1StructureVisibility(false);
        SetLibraryStructureVisibility(false);

        this.gameObject.SetActive(visible);
    }

    private IEnumerator DirectionIndicatorAppear(DirectionIndicator indicator)
    {
        indicator.Active = true;

        int waitTime = 0;

        while (waitTime < 2)
        {
            waitTime++;

            if (waitTime < 2)
            {
                yield return new WaitForSeconds(1);
            }
            else
            {
                Debug.Log(waitTime);

                //indicator.Active = false;
                //indicator.gameObject.SetActive(false);

                //StopCoroutine(coroutine);
            }
        } 
    }

    public void SetUserPosition()
    {
        Vector3 userPosition = new Vector3();
        string tip = "";

        switch(UserCurrentPosition)
        {
            case UserPosition.Building1:
                userPosition = this.transform.position + new Vector3(-0.22f, 0.0f, -0.443f);
                tip = "主楼";
                break;

            case UserPosition.Gymnastic:
                userPosition = this.transform.position + new Vector3(-0.524f, 0.0f, -0.053f);
                tip = "体育馆";
                break;

            case UserPosition.Library:
                userPosition = this.transform.position + new Vector3(0.541f, 0.0f, -0.053f);
                tip = "图书馆";
                break;

            case UserPosition.Building2:
                userPosition = this.transform.position + new Vector3(-0.22f, 0.0f, -0.443f);
                tip = "二号楼";
                break;
        }

        charactor.transform.position = userPosition;

        //MainScreenManager.Instance.SetLocationTip(tip);
    }

    public void SetBuilding1StructureVisibility(bool visible)
    {
        //building1StructureIndicator.GetComponent<DirectionIndicator>().Active = visible;

        building1Structure.SetActive(visible);

        MeshRenderer[] marr = building1Structure.GetComponentsInChildren<MeshRenderer>(true);
        foreach (MeshRenderer m in marr)
        {
            m.enabled = visible;
        }
    }

    public void SetLibraryStructureVisibility(bool visible)
    {
        //libraryStructureIndicator.GetComponent<DirectionIndicator>().Active = visible;

        libraryStructure.SetActive(visible);

        MeshRenderer[] marr = libraryStructure.GetComponentsInChildren<MeshRenderer>(true);
        foreach (MeshRenderer m in marr)
        {
            m.enabled = visible;
        }
    }

    private void SetTag()
    {
        foreach (Transform child in building1.transform)
        {
            child.gameObject.tag = "Building1";

            foreach (Transform c in child.transform)
            {
                c.gameObject.tag = "Building1";
            }
        }

        foreach (Transform child in library.transform)
        {
            child.gameObject.tag = "Library";

            foreach (Transform c in child.transform)
            {
                c.gameObject.tag = "Library";
            }
        }

        foreach (Transform child in gym.transform)
        {
            child.gameObject.tag = "Gym";

            foreach (Transform c in child.transform)
            {
                c.gameObject.tag = "Gym";
            }
        }
    }
}

public enum UserPosition
{
    Library = 0,
    Building1,
    Gymnastic,
    Building2
};
