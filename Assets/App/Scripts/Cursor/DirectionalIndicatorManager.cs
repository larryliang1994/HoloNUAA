using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit;

public class DirectionalIndicatorManager : MonoBehaviour
{
    public static DirectionalIndicatorManager Instance { get; private set; }

    public GameObject Indicator;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	public void ShowIndicator(GameObject target)
    {
        //target.AddComponent()
    }

    public void HideIndicator()
    {

    }
}
