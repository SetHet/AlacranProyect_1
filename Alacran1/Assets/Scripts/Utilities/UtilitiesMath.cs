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
            return Mathf.Abs(Mathf.Sin(Mathf.PI*x/long_wave));
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
            public List<Vector2> points;
            public TypeConnection typeConnection = TypeConnection.lineal;
            public enum TypeConnection
            {
                lineal, sin
            }

            public void AddPoint(float x, float y)
            {
                AddPoint(new Vector2(x, y));
            }

            public void AddPoint(Vector2 point)
            {
                points.Add(point);
                points.Sort(); //falta decir que se ordene segun la x
            }
            
            public Vector2[] GetPoints()
            {
                return points.ToArray();
            }

            public float GetValue(float x)
            {
                return 0;
            }
        }
    }
    
}