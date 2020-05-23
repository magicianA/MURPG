using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Right : MonoBehaviour
{
    InteractiveItem knife;
    Backpack backpack;
    public TextAsset text;
    private bool check = false;
    private float timer = 0f;
    private List<string> checkList = new List<string>();
    private void Start()
    {
        backpack = GameObject.FindWithTag("Backpack").GetComponent<Backpack>();
        knife = new InteractiveItem("Daily/道具/刀","刀");
        checkList.Clear();
    }
    private void Update()
    {
        if (check&&!backpack.ContainsItem("刀"))
        {
            timer += Time.deltaTime ;
            if(MyInput.isButtonDown)
            {
                if (timer > 2.25f && timer < 3.75f)
                {
                    checkList.Add("长");
                }

                else if (timer > 0.5f && timer < 2f)
                {
                    checkList.Add("短");
                }
                else
                {
                    checkList.Clear();
                }
                timer = 0;
                
            }
            if (checkList.Count == 4)
            {
                if (checkList[0] == "长" && checkList[1] == "短"  &&
                    checkList[2] == "长" && checkList[3] == "短" )
                {
                    backpack.AddItem(knife);
                    check = false;
                }
                else
                    checkList.Clear();
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
