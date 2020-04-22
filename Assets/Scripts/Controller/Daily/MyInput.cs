using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyInput : MonoBehaviour
{
    public static bool isButtonDown;
    private void Update()
    {
        isButtonDown = false;
        if (Input.GetKeyDown(KeyCode.F))
            isButtonDown = true;
    }
    
}
