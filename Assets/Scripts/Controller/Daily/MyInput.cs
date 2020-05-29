using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyInput : MonoBehaviour
{
    public static bool isButtonDown;
    public static bool w;
    public static bool a;
    public static bool s;
    public static bool d;
    private void Update()
    {
        isButtonDown = false;
        if(DirBtn.f)
        {
            isButtonDown = true;
            DirBtn.f = false;
        }
    }
   
}
