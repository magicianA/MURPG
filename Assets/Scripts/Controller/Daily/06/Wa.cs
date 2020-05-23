using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wa : MonoBehaviour
{
    InteractiveItem wa;
    Backpack backpack;

    public GameObject next;
    void Start()
    {
        backpack = GameObject.FindWithTag("Backpack").GetComponent<Backpack>();
        wa = new InteractiveItem("Daily/道具/哇", "哇");
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(MyInput.isButtonDown)
        {
            backpack.AddItem(wa);
            next.SetActive(true);
            Destroy(gameObject);
        }
    }
}
