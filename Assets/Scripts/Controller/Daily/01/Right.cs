using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Right : MonoBehaviour
{
    InteractiveItem knife;
    Backpack backpack;
    public TextAsset text;
    private bool check = false;
    private int count = 0;
    private List<string> checkList = new List<string>();
    private void Start()
    {
        backpack = GameObject.FindWithTag("Backpack").GetComponent<Backpack>();
        knife = new InteractiveItem("Daily/道具/刀","刀");
        checkList.Clear();
        count = 0;
    }
    private void Update()
    {
        if (check&&!backpack.ContainsItem("刀"))
        {
            if(MyInput.isButtonDown)
            {
                count++;
                Debug.Log(count);
            }
            if (count == 4)
            {
                backpack.AddItem(knife);
                check = false;
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if (MyInput.isButtonDown&&!backpack.itemsGameObjects.ContainsKey("刀")&&UIManager.isFinish)
        {
            UIManager.Dialogue(text);
            gameObject.GetComponent<AudioSource>().Play();
            //backpack.AddItem(knife);

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        check = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        check = false;
    }
}
