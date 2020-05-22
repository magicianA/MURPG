using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Left : MonoBehaviour
{
    InteractiveItem ma;
    Backpack backpack;
    public TextAsset text;
    private void Start()
    {
        backpack = GameObject.FindWithTag("Backpack").GetComponent<Backpack>();
        ma = new InteractiveItem("Daily/道具/玛","玛");

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (MyInput.isButtonDown && !backpack.itemsGameObjects.ContainsKey("玛"))
        {
            UIManager.Dialogue(text);
            backpack.AddItem(ma);
        }
    }
}
