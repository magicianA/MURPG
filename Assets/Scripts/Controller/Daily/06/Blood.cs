using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blood : MonoBehaviour
{
    InteractiveItem la;
    Backpack backpack;
    public TextAsset text;

    public GameObject obj;
    private void Start()
    {
        backpack = GameObject.FindWithTag("Backpack").GetComponent<Backpack>();
        la = new InteractiveItem("Daily/道具/拉", "拉");
    }
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (MyInput.isButtonDown) 
        {
            UIManager.Dialogue(text);
            backpack.AddItem(la);
            obj.SetActive(true);
            DailyPlayer.camera.GetComponent<Camera>().orthographicSize = 14f;
            DailyPlayer.camera.GetComponent<SmartCamera>().enabled = false;
        }

    }
}
