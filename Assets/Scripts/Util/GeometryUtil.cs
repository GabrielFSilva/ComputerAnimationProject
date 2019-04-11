using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GFS.Util
{
    public class GeometryUtil : MonoBehaviour
    {

        public static bool PointToTheLeftOfLine(Vector2 point, Vector2 lineA, Vector2 lineB)
        {
            float val1 = (lineB.x - lineA.x) * (point.y - lineA.y);
            float val2 = (lineB.y - lineA.y) * (point.x - lineA.x);
            return (val1 - val2) >= 0f;
        }

        public static bool PointToTheRightOfLine(Vector2 point, Vector2 lineA, Vector2 lineB)
        {
            return !PointToTheLeftOfLine(point, lineA, lineB);
        }

        public static bool PointInsideTriangle(Vector2 point, Vector2 vertexA, Vector2 vertexB, Vector2 vertexC)
        {
            return PointToTheLeftOfLine(point, vertexA, vertexB)
                && PointToTheLeftOfLine(point, vertexB, vertexC)
                && PointToTheLeftOfLine(point, vertexC, vertexA);
        }

        public static Vector2 TriangleCentroid(Vector2 vertexA, Vector2 vertexB, Vector2 vertexC)
        {
            return (vertexA + vertexB + vertexC) / 3f;
        }

        public bool LineIntersection(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4, 
            out bool lines_intersect, out bool segments_intersect, out Vector2 intersection, 
            out Vector2 close_p1, out Vector2 close_p2)
        {
            // Get the segments' parameters.
            float dx12 = p2.x - p1.x;
            float dy12 = p2.y - p1.y;
            float dx34 = p4.x - p3.x;
            float dy34 = p4.y - p3.y;

            // Solve for t1 and t2
            float denominator = (dy12 * dx34 - dx12 * dy34);

            float t1 =
                ((p1.x - p3.x) * dy34 + (p3.y - p1.y) * dx34)
                    / denominator;
            if (denominator < 0.001f || float.IsInfinity(t1))
            {
                // The lines are parallel (or close enough to it).
                lines_intersect = false;
                segments_intersect = false;
                intersection = new Vector2(float.NaN, float.NaN);
                close_p1 = new Vector2(float.NaN, float.NaN);
                close_p2 = new Vector2(float.NaN, float.NaN);
                return false;
            }
            lines_intersect = true;

            float t2 =
                ((p3.x - p1.x) * dy12 + (p1.y - p3.y) * dx12)
                    / -denominator;

            // Find the point of intersection.
            intersection = new Vector2(p1.x + dx12 * t1, p1.y + dy12 * t1);

            // The segments intersect if t1 and t2 are between 0 and 1.
            segments_intersect =
                ((t1 >= 0) && (t1 <= 1) &&
                 (t2 >= 0) && (t2 <= 1));

            // Find the closest points on the segments.
            if (t1 < 0)
            {
                t1 = 0;
            }
            else if (t1 > 1)
            {
                t1 = 1;
            }

            if (t2 < 0)
            {
                t2 = 0;
            }
            else if (t2 > 1)
            {
                t2 = 1;
            }

            close_p1 = new Vector2(p1.x + dx12 * t1, p1.y + dy12 * t1);
            close_p2 = new Vector2(p3.x + dx34 * t2, p3.y + dy34 * t2);

            return segments_intersect;
        }

        public static bool LineIntersection(Vector2 firstLineA, Vector2 firstLineB, Vector2 secondLineA, Vector2 secondLineB, 
            out float valueFirst, out float valueSecond)
        {
            float determinant = (firstLineB.x * secondLineB.y) - (firstLineA.y * firstLineB.x);

            if (Mathf.Abs(determinant) < 0.001f) // No Intersection
            {
                valueFirst = float.NaN;
                valueSecond = float.NaN;
                return false;
            }
            else // Intersection
            {
                valueFirst = (firstLineA.y - secondLineB.y) * secondLineB.x - (firstLineA.x - secondLineA.x) * secondLineB.y / determinant;
                
                if (Mathf.Abs(secondLineB.x) >= 0.001f)
                {
                    valueSecond = (firstLineA.x + valueFirst * firstLineB.x - secondLineA.x) / secondLineB.x;
                }
                else
                {
                    valueSecond = (firstLineA.y + valueFirst * firstLineB.y - secondLineA.y) / secondLineB.y;
                }
            }
            return true;
        }

        public static float PolygonArea(List<Vector2> vertexList)
        {
            if (vertexList.Count < 3)
                Debug.LogError("Not enought vertices");

            float areaSum = 0f;
            int j = 1;

            for (int i = 0; i < vertexList.Count; i++)
            {
                if (i < vertexList.Count - 1)
                    j = i + 1;
                else
                    j = 0;

                Vector2 pointA = vertexList[i];
                Vector2 pointB = vertexList[j];
                areaSum += (pointA.x * pointB.y) - (pointA.y * pointB.x);
            }

            Debug.Log("Area:" + (0.5f * areaSum));
            return 0.5f * areaSum;
        }
        
        public static bool PointInsideCircumcircle(Vector2 p, Vector2 c0, Vector2 c1, Vector2 c2)
        {
            var ax = c0.x - p.x;
            var ay = c0.y - p.y;
            var bx = c1.x - p.x;
            var by = c1.y - p.y;
            var cx = c2.x - p.x;
            var cy = c2.y - p.y;

            return (
                    (ax * ax + ay * ay) * (bx * cy - cx * by) -
                    (bx * bx + by * by) * (ax * cy - cx * ay) +
                    (cx * cx + cy * cy) * (ax * by - bx * ay)
            ) > 0;
        }

        public static Vector2 CircumcircleCenter(Vector2 c0, Vector2 c1, Vector2 c2)
        {
            var mp0 = 0.5f * (c0 + c1);
            var mp1 = 0.5f * (c1 + c2);

            var v0 = VectorUtil.RotateRightAngle(c0 - c1);
            var v1 = VectorUtil.RotateRightAngle(c1 - c2);

            float m0, m1;

            LineIntersection(mp0, v0, mp1, v1, out m0, out m1);

            return mp0 + m0 * v0;
        }
    }
}
