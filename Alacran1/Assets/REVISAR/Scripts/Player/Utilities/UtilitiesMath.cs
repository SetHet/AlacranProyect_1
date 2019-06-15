using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    public class UtilitiesMath
    {
        public static float SenAbs2(float x, float long_wave = 1f)
        {
            if (long_wave == 0) return 0f;
            return Mathf.Abs(Mathf.Sin(Mathf.PI * x / long_wave));
        }

        public static float RemapFloat(float value, float preRangeMin = 0f, float preRangeMax = 1f, float posRangeMin = 0f, float posRangeMax = 1f)
        {
            float percent = 0f;
            float Dif = preRangeMax - preRangeMin;
            if (Dif != 0) percent = value - preRangeMin / Dif;
            else return posRangeMin;

            Dif = posRangeMax - posRangeMin;
            return (Dif * percent) + posRangeMin;
        }

        [System.Serializable]
        public class AlgoritmoForPoint
        {
            public List<Vector2> points = new List<Vector2>();
            public TypeConnection typeConnection = TypeConnection.lineal;
            [Range(0f, 1f)]
            public float blendSmooth = 0.5f;
            public enum TypeConnection
            {
                lineal, smooth, blend
            }

            public void SetType(TypeConnection type, float valueBlend = 0.5f)
            {
                typeConnection = type;
                SetBlend(valueBlend);
            }

            public void SetBlend(float valueBlend = 0.5f)
            {
                if (valueBlend < 0f) blendSmooth = 0f;
                else if (valueBlend > 1f) blendSmooth = 1f;
                else blendSmooth = valueBlend;
            }

            public void AddPoint(float x, float y)
            {
                AddPoint(new Vector2(x, y));
            }

            public void AddPoint(Vector2 point)
            {
                points.Add(point);
                points.Sort((a,b) => a.x.CompareTo(b.x));
            }

            public Vector2[] GetPoints()
            {
                return points.ToArray();
            }

            public float GetValue(float x)
            {
                if (points.Count == 0) return 0f;
                
                for (int i = 0; i < points.Count - 1; i++)
                {
                    if (x >= points[i + 1].x) continue;
                    if (x <= points[i].x) return points[i].y;

                    return RemapFloat(GetPreY(x, i), 0, 1, points[i].y, points[i+1].y);
                }

                if (x >= points[points.Count - 1].x) return points[points.Count - 1].y;

                return 0;
            }

            public float GetPreY(float x, int index)
            {
                float distAB = points[index + 1].x - points[index].x;
                if (distAB == 0f) return 0f;
                float distAX = x - points[index].x;
                float rad = 0f;
                switch (typeConnection)
                {
                    case TypeConnection.lineal:
                        return distAX / distAB;
                        //break;
                    case TypeConnection.smooth:
                        rad = distAX * Mathf.PI / distAB;
                        return ((-Mathf.Cos(rad))+1)/2f;
                    //break;
                    case TypeConnection.blend:
                        float valLineal = distAX / distAB;
                        rad = valLineal * Mathf.PI;
                        float valSmooth = ((-Mathf.Cos(rad)) + 1) / 2f;
                        return (valLineal * (1f - blendSmooth)) + (valSmooth * blendSmooth);
                    default:
                        return 0f;
                        //break;
                }
            }
        }
    }

}
