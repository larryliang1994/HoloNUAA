using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LocalJoost.HoloToolkitExtensions;

public class TapToPlaceParent : MonoBehaviour
{
    public GameObject parent;
    public bool IsActive = true;

    bool placing = false;

    // Called by GazeGestureManager when the user performs a Select gesture
    void OnSelect()
    {
        if (IsActive)
        {
            MoveByGaze moveByGaze = parent.GetComponent<MoveByGaze>();

            if (!placing)
            {
                placing = true;
                moveByGaze.IsActive = true;
            }
            else
            {
                placing = false;
            }
        }
    }

}
