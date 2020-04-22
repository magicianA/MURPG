using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Right : MonoBehaviour
{
    InteractiveItem knife;
    Backpack backpack;
    private void Start()
    {
        backpack = GameObject.FindWithTag("Backpack").GetComponent<Backpack>();
        knife = new InteractiveItem("Daily/主寺外/knife","knife","刀");

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (MyInput.isButtonDown&&!backpack.itemsGameObjects.ContainsKey("knife"))
        {
            backpack.AddItem(knife);
        }
    }
}
