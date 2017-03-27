using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public static PlatformManager Instance { get; private set; }

    public GameObject building1;
    public GameObject library;
    public GameObject gym;
    public GameObject building1Structure;
    public GameObject charactor;

    // Use this for initialization
    void Start ()
    {
        Instance = this;

        SetTag();

        SetVisibility(false);
    }

    public void SetVisibility(bool visible)
    {
        this.gameObject.SetActive(visible);

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

        MeshRenderer[] marr = this.GetComponentsInChildren<MeshRenderer>(true);
        foreach (MeshRenderer m in marr)
        {
            m.enabled = visible;
        }
    }

    public void SetBuilding1StructureVisibility(bool visible)
    {
        building1Structure.SetActive(visible);

        MeshRenderer[] marr = building1Structure.GetComponentsInChildren<MeshRenderer>(true);
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

        foreach (Transform child in building1Structure.transform)
        {
            child.gameObject.tag = "Building1_Structure";

            foreach (Transform c in child.transform)
            {
                c.gameObject.tag = "Building1_Structure";
            }
        }
    }
}
