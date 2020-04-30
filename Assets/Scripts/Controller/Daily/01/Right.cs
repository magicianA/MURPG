using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Right : MonoBehaviour
{
    InteractiveItem knife;
    Backpack backpack;
    public TextAsset text;
    private void Start()
    {
        backpack = GameObject.FindWithTag("Backpack").GetComponent<Backpack>();
        knife = new InteractiveItem("Daily/道具/刀","刀");

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (MyInput.isButtonDown&&!backpack.itemsGameObjects.ContainsKey("刀"))
        {
            UIManager.Dialogue(text);
            backpack.AddItem(knife);
        }
    }
}
