using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Left : MonoBehaviour
{
    InteractiveItem ma;
    Backpack backpack;
    public TextAsset text;

    private bool check = false;
    private float timer = 0f;
    private List<string> checkList = new List<string>();
    private void Start()
    {
        backpack = GameObject.FindWithTag("Backpack").GetComponent<Backpack>();
        ma = new InteractiveItem("Daily/道具/玛", "玛");

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (MyInput.isButtonDown && !backpack.itemsGameObjects.ContainsKey("玛"))
        {
            UIManager.Dialogue(text);
        }
    }
    private void Update()
    {
        if (check && !backpack.ContainsItem("玛"))
        {
            timer += Time.deltaTime;
            if (MyInput.isButtonDown)
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
                if (checkList[0] == "长" && checkList[1] == "短" &&
                    checkList[2] == "短" && checkList[3] == "长")
                {
                    backpack.AddItem(ma);
                    check = false;
                }
                else
                    checkList.Clear();
            }
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
