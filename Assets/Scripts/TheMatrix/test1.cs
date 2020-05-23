using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test1 : MonoBehaviour
{
    Backpack backpack;
    InteractiveItem ma;
    InteractiveItem yang;
    InteractiveItem la;
    InteractiveItem ha;
    InteractiveItem wa;
    private void Start()
    {
        backpack = GameObject.FindWithTag("Backpack").GetComponent<Backpack>();
        ma = new InteractiveItem("Daily/道具/玛", "玛");
        backpack.AddItem(ma);
        yang  = new InteractiveItem("Daily/道具/洋", "洋");
        backpack.AddItem(yang);
        la = new InteractiveItem("Daily/道具/拉", "拉");
        backpack.AddItem(la);
        ha = new InteractiveItem("Daily/道具/哈", "哈");
        backpack.AddItem(ha);
        wa = new InteractiveItem("Daily/道具/哇", "哇");
        backpack.AddItem(wa);
    }
}
