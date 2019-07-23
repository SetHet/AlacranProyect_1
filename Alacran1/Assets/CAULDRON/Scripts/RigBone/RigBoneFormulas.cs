using UnityEngine;
using System.Collections;

namespace RigBone
{
    public class Formulas
    {
        public static float TriangleAngle(float aLen, Vector3 v1, Vector3 v2)
        {
            float aLen1 = v1.magnitude;
            float aLen2 = v2.magnitude;
            aLen = Mathf.Clamp(aLen, 0, aLen1 + aLen2);
            float c = Mathf.Clamp((aLen1 * aLen1 + aLen2 * aLen2 - aLen * aLen) / ((aLen1 * aLen2) * 2.0f), -1.0f, 1.0f);
            return Mathf.Acos(c) * Mathf.Rad2Deg;
        }

        public static void Rotate(Transform obj, Quaternion rotation)
        {
            obj.rotation = rotation * obj.rotation;
        }

        public static void newDireccion(Transform t, Vector3 da, Vector3 db)
        {
            Vector3 axis = Vector3.Cross(da, db);
            float angle = Vector3.SignedAngle(da, db, axis);
            t.Rotate(axis, angle, Space.World);
        }

        public static void InRange(ref float value, float min, float max)
        {
            if (value < min) value = min;
            else if (value > max) value = max;
        }

        public static Vector3 GetDirPos(Transform a, Transform b)
        {
            return b.transform.position - a.transform.position;
        }

        public static void DrawBone(Transform a, Transform b)
        {
            Debug.DrawLine(a.transform.position, b.transform.position, Color.red);
        }

        public static void DrawAxis(Transform point, Vector3 v)
        {
            Debug.DrawLine(point.transform.position, point.transform.position + v, Color.blue);
        }

    }
}