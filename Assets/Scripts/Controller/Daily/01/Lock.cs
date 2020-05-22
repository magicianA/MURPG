using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour
{
    Backpack backpack;
    public TextAsset text;
    public GameObject next;
    private void Start()
    {
        backpack = GameObject.FindWithTag("Backpack").GetComponent<Backpack>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (MyInput.isButtonDown == true)
        {
            if (backpack.itemsGameObjects.ContainsKey("玛"))
            {
                next.SetActive(true);
                Destroy(gameObject);
            }

            else
                UIManager.Dialogue(text);
        }
    }
}
