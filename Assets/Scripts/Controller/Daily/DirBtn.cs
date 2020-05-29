using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DirBtn : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool w = false;
    private bool a = false;
    private bool s = false;
    private bool d = false;
    public static bool f = false;
    public void OnPointerDown(PointerEventData eventData)
    {
        switch (this.name)
        {
            case "w":
                MyInput.w = true;
                break;
            case "a":
                MyInput.a = true;
                break;
            case "s":
                MyInput.s = true;
                break;
            case "d":
                MyInput.d = true;
                break;
            case "f":
                f = true;
                break;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        switch (this.name)
        {
            case "w":
                MyInput.w = false;
                break;
            case "a":
                MyInput.a = false;
                break;
            case "s":
                MyInput.s = false;
                break;
            case "d":
                MyInput.d = false;
                break;
            case "f":
                f = false;
                break;
        }
    }
    void LateUpdate()
    {
    }
}
