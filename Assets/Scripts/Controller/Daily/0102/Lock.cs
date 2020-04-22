using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour
{
    Backpack backpack;
    private void Start()
    {
        backpack = GameObject.FindWithTag("Backpack").GetComponent<Backpack>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (MyInput.isButtonDown == true)
        {
            if (backpack.itemsGameObjects.ContainsKey("ma"))
                Destroy(gameObject);
            else 
                UIManager.TextTip("门已上锁");
        }
    }
}
