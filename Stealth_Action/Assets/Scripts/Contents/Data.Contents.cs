using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataContents
{
    public struct ViewCastInfo
    {
        public bool Hit;
        public float Angle;
        public float Distance;
        public Vector3 Point;

        public ViewCastInfo(bool hit, float angle, float distance, Vector3 point)
        {
            Hit = hit;
            Angle = angle;
            Distance = distance;
            Point = point;
        }
    }
}
