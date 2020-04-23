using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Left : MonoBehaviour
{
    InteractiveItem ma;
    Backpack backpack;
    private void Start()
    {
        backpack = GameObject.FindWithTag("Backpack").GetComponent<Backpack>();
        ma = new InteractiveItem("Daily/Outside/ma","ma", "玛");

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (MyInput.isButtonDown && !backpack.itemsGameObjects.ContainsKey("ma"))
        {
            backpack.AddItem(ma);
            UIManager.TextTip("获得玛");
        }
    }
}
