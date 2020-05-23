using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hanging : MonoBehaviour
{
    public TextAsset text;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (MyInput.isButtonDown&&UIManager.isFinish)
        {
            UIManager.Dialogue(text);
            gameObject.GetComponent<AudioSource>().Play();
        }

    }
}
