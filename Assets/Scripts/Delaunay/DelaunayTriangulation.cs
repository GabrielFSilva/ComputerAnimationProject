using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GFS.Util;

namespace GFS.Delaunay
{
    public class DelaunayTriangulation
    {
        public readonly List<Vector2> vertexList;
        public readonly List<int> triangleList;

        private DelaunayTriangulation()
        {
            vertexList = new List<Vector2>();
            triangleList = new List<int>();
        }

        private void ClearLists()
        {
            vertexList.Clear();
            triangleList.Clear();
        }

        public bool VerifyTriangulation()
        {
            try
            {
                for (int i = 0; i < triangleList.Count; i += 3)
                {
                    var c0 = vertexList[triangleList[i]];
                    var c1 = vertexList[triangleList[i + 1]];
                    var c2 = vertexList[triangleList[i + 2]];

                    for (int j = 0; j < vertexList.Count; j++)
                    {
                        var p = vertexList[j];
                        if (GeometryUtil.PointInsideCircumcircle(p, c0, c1, c2))
                        {
                            return false;
                        }
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}