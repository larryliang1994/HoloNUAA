using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationPoints : MonoBehaviour
{
    public static List<Vector3> DemoPoints = new List<Vector3>();

    public static Vector3 A, BR, BL, C, D, E, F;

    public static void InitPointsData(float scale)
    {
        A = new Vector3(0.0f, -0.5f, 0.0f);
        BR = new Vector3(56.0f * scale, -0.5f, 0.0f * scale);
        BL = new Vector3(-56.0f * scale, -0.5f, 0.0f * scale);

        C = new Vector3(56.0f * scale, -0.5f, 29.0f * scale);
        D = new Vector3(64.0f * scale, -0.5f, 29.0f * scale);

        E = new Vector3(64.0f * scale, 3.5f, 29.0f * scale);
        F = new Vector3(58.0f * scale, 3.5f, 29.0f * scale);
       
        // 原点
        DemoPoints.Add(new Vector3(0.0f, -0.5f, 0.0f));

        // 一楼
        DemoPoints.Add(new Vector3(56.0f * scale, -0.5f, 0.0f * scale));
        DemoPoints.Add(new Vector3(56.0f * scale, -0.5f, 29.0f * scale));
        DemoPoints.Add(new Vector3(64.0f * scale, -0.5f, 29.0f * scale));

        // 二楼
        DemoPoints.Add(new Vector3(64.0f * scale, 3.5f, 29.0f * scale));
        DemoPoints.Add(new Vector3(58.0f * scale, 3.5f, 29.0f * scale));
        DemoPoints.Add(new Vector3(58.0f * scale, 3.5f, 55.0f * scale));
        DemoPoints.Add(new Vector3(63.0f * scale, 3.5f, 55.0f * scale));
    }

    public static List<Vector3> GetBooksPoints(Book book, float scale)
    {
        List<Vector3> points = new List<Vector3>();

        points.Add(A);
        
        if (book.Floor == 1)
        {
            // 右边
            if (book.BookshelfNum == 0)
            {
                points.Add(BR);
                points.Add(C);
            }
            // 左边
            else if(book.BookshelfNum == 1)
            {
                points.Add(BL);
            }
        }
        else if (book.Floor == 2)
        {
            // 到楼梯
            points.Add(BR);
            points.Add(C);

            // 上楼
            points.Add(D);
            points.Add(E);
            points.Add(F);

            // 右边
            if (book.BookshelfNum == 0)
            {
                // 走到书架旁
                points.Add(new Vector3(58.0f * scale, 3.5f, F.z + (16 + book.DistanceCount * 2) * scale));

                if (book.Direction == 1)
                {
                    Vector3 lastPoint = points[points.Count - 1];

                    points.Add(new Vector3(lastPoint.x + 5 * scale, lastPoint.y, lastPoint.z));
                }
            }
            // 左边
            else if (book.BookshelfNum == 1)
            {

            }
        }

        return points;
    }
}
