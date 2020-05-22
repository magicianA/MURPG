using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ha : MonoBehaviour
{
    InteractiveItem ha;
    Backpack backpack;
    public GameObject next;
    private void Start()
    {
        backpack = GameObject.FindWithTag("Backpack").GetComponent<Backpack>();
        ha = new InteractiveItem("Daily/道具/哈", "哈");

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (MyInput.isButtonDown)
        {
            backpack.AddItem(ha);
            next.SetActive(true);
        }
    }
}
