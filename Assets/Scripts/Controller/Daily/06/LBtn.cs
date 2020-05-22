using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LBtn : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (MyInput.isButtonDown)
            transform.GetComponentInParent<Turntable>().SendMessage("GetMessage", "wise");
    }
}
