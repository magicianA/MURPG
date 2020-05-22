using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throne : MonoBehaviour
{
    public TextAsset text;
    private GameObject throne;
    public GameObject[] runes;
    private void Start()
    {
        throne = DailyPlayer.throne;
        CheckRunes();
    }
    private void Update()
    {
        CheckRunes();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (MyInput.isButtonDown)
        {
            UIManager.Dialogue(text);
            DailyPlayer.movable = false;
            throne.SetActive(true);
            throne.GetComponent<ThroneShow>().UpdateItems();
        }
    }

    private void CheckRunes()
    {
        for(int i = 0; i < runes.Length; i++)
        {
            runes[i].SetActive(false);
            if (ThroneData.throneItemList.Contains(runes[i].name))
                runes[i].SetActive(true);
        }
    }
}
