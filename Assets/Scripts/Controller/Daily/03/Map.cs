using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public TextAsset text;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (MyInput.isButtonDown)
        {
            UIManager.Dialogue(text);
            if (gameObject.name != "Map")
                DailyPlayer.map.SetActive(true);
        }
    }
}
