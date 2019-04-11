using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GFS.Util
{
    public static class VectorUtil
    {
        public static bool IsReal(this float f)
        {
            return !float.IsInfinity(f) && !float.IsNaN(f);
        }
        public static bool IsReal(this Vector2 v)
        {
            return v.x.IsReal() && v.y.IsReal();
        }
        public static bool IsReal(this Vector3 v)
        {
            return v.x.IsReal() && v.y.IsReal() && v.z.IsReal();
        }

        public static Vector2 RotateRightAngle(this Vector2 vector)
        {
            float x = vector.x;
            vector.x = -vector.y;
            vector.y = x;

            return vector;
        }
    }
}