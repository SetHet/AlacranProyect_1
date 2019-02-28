using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    public static class UtilitiesMath
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
    }
}