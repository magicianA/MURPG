using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapShow : MonoBehaviour
{
    private void Update()
    {
        if (DailyPlayer.movable == true)
            DailyPlayer.movable = false;
    }
    public void OnClose()
    {
        gameObject.SetActive(false);
        DailyPlayer.movable = true;
    }
}
