using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoGUI : MonoBehaviour
{
    public bool desabilitar = false;
    public List<string> mensajes;

    [Header("Rect")]
    public float height = 20;
    public float width = 100;
    public float space = 2;
    public Vector2 offset = new Vector2(5, 5);

    private void OnGUI()
    {
        if (desabilitar) return;
        Rect rect = new Rect();
        int i = 0;
        foreach (var item in mensajes)
        {
            rect.Set(offset.x, offset.y + height * i + space * i, width, height);
            GUI.Label(rect, item);
            i++;
        }
    }
}
