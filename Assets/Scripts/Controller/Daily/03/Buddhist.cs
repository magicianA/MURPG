using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buddhist : MonoBehaviour
{
    public TextAsset text;

    InteractiveItem a;
    Backpack backpack;
    public GameObject next;
    private void Start()
    {
        backpack = GameObject.FindWithTag("Backpack").GetComponent<Backpack>();
        a = new InteractiveItem("Daily/道具/阿", "阿");
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (MyInput.isButtonDown)
        {
            UIManager.Dialogue(text);
            DailyPlayer.buddhist.SetActive(true);
            if (!backpack.ContainsItem("阿"))
            {
                backpack.AddItem(a);
            }
        }
    }
    private void Update()
    {
        if (DailyPlayer.isBuddhist)
            next.SetActive(true);
    }
}
