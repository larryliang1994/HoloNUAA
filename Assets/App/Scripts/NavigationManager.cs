using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationManager : MonoBehaviour
{
    public static NavigationManager Instance { get; private set; }

    public Material DotMaterial;
    public float DotSize;
    public float Scale;

    private List<GameObject> dots;

    // Use this for initialization
    private void Start ()
    {
        Instance = this;

        dots = new List<GameObject>();

        NavigationPoints.InitPointsData(Scale);
    }

    public void StartNavigation(string id)
    {
        ShowNavigationDot(NavigationPoints.GetBooksPoints(Books.FindBookById(id), Scale));
    }

    private void ShowNavigationDot(List<Vector3> points)
    {
        // 转折点
        foreach(Vector3 point in points)
        {
            AddDot(point);
        }

        // 普通点
        for (int i = 0; i < points.Count - 1; i++)
        {
            if (points[i].y == points[i + 1].y && points[i].z == points[i + 1].z)
            {
                float startPointX = points[i].x < points[i + 1].x ? points[i].x : points[i + 1].x;

                for (float j = 0; j < Mathf.Abs(points[i + 1].x - points[i].x) / Scale; j += 0.2f)
                {
                    AddDot(new Vector3(startPointX + j * Scale, points[i].y, points[i].z));
                }
            }
            else if (points[i].x == points[i + 1].x && points[i].z == points[i + 1].z)
            {
                float startPointY = points[i].y < points[i + 1].y ? points[i].y : points[i + 1].y;

                for (float j = 0; j < Mathf.Abs(points[i + 1].y - points[i].y) / Scale; j += 0.2f)
                {
                    AddDot(new Vector3(points[i].x, points[i].y + j * Scale, points[i].z));
                }
            }
            else if (points[i].x == points[i + 1].x && points[i].y == points[i + 1].y)
            {
                float startPointZ = points[i].z < points[i + 1].z ? points[i].z : points[i + 1].z;

                for (float j = 0; j < Mathf.Abs(points[i + 1].z - points[i].z) / Scale; j += 0.2f)
                {
                    AddDot(new Vector3(points[i].x, points[i].y, points[i].z + j * Scale));
                }
            }
        }
    }

    public void HideNavigation()
    {
        foreach(GameObject dot in dots)
        {
            Destroy(dot);
        }

        MainScreenManager.Instance.SetScreenTip(ScreenTipContent.ShowMenu);
    }

    private void AddDot(Vector3 position)
    {
        GameObject dot = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        Vector3 cameraPosition = Camera.main.transform.position + Camera.main.transform.forward.normalized;

        dot.GetComponent<MeshRenderer>().material = DotMaterial;
        dot.transform.localScale = new Vector3(DotSize, DotSize, DotSize);
        dot.transform.position = position + cameraPosition;

        dots.Add(dot);
    }
}
