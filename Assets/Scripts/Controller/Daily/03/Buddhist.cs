﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buddhist : MonoBehaviour
{
    public TextAsset text;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (MyInput.isButtonDown)
        {
            UIManager.Dialogue(text);
            DailyPlayer.buddhist.SetActive(true);
        }
    }
}
