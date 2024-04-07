using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    Canvas canvas;
    private void Awake()
    {
        canvas = GetComponent<Canvas>();
    }
    public void EnableCanvas()
    {
        canvas.enabled = !canvas.enabled;
    }
}
