using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buliding : MonoBehaviour
{
    public TextAsset text;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(true)
            UIManager.Dialogue(text);
    }
}
