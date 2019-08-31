using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LookRendimiento : MonoBehaviour
{
    public Text textFPS;
    public Text textTF;

    public string infoFPS = "FPS: ";
    public string infoTF = "MS: ";
    

    private void Update()
    {
        textFPS.text = infoFPS + ((int)(10 / Time.deltaTime))/10f + "fps";
        textTF.text = infoTF + ((int)(10000 * Time.deltaTime)) / 10f + " ms";
    }
}
