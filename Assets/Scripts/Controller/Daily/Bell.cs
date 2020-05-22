using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bell : MonoBehaviour
{
    private float originTime = 0;
    /// <summary>
    /// 最小
    /// </summary>
    public float highSpeedLowerLimit;
    /// <summary>
    /// 次小
    /// </summary>
    public float highSpeedUpperLimit;
    /// <summary>
    /// 次大
    /// </summary>
    public float lowSpeedLowerLimit;
    /// <summary>
    /// 最大
    /// </summary>
    public float lowSpeedUpperLimit;
    private int lowCount = 0;
    private int midCount = 0;
    private int highCount = 0;
    bool inRegion = false;
    bool lowFinish = false;
    bool midFinish = false;
    bool highFinish = false;

    InteractiveItem yang;
    Backpack backpack;

    public GameObject tilt;

    public AudioClip[] clips;
    private void Start()
    {
        backpack = GameObject.FindWithTag("Backpack").GetComponent<Backpack>();
        yang = new InteractiveItem("Daily/道具/洋", "洋");
    }
    private void Update()
    {
        if (MyInput.isButtonDown == true && inRegion)
        {
            PlaySound();
            float interval = Time.time - originTime;
            Debug.Log(interval);
            originTime = Time.time;
            //高速
            if (highSpeedLowerLimit < interval && interval < highSpeedUpperLimit&&!highFinish)
            {
                HighSpeed();
            }
            //中速
            else if (highSpeedUpperLimit < interval && interval < lowSpeedLowerLimit&&!midFinish)
            {
                MidSpeed();
            }
            //低速
            else if (lowSpeedLowerLimit < interval && interval < lowSpeedUpperLimit&&!lowFinish)
            {
                LowSpeed();
            }
            else
            {
                lowCount = 0;
                midCount = 0;
                highCount = 0;
            }

            Debug.Log("lowcount " + lowCount + ";   midcount" + midCount + ";   highcount" + highCount);


        }
        if (lowFinish && midFinish && highFinish)
        {
            tilt.SetActive(true);
            backpack.AddItem(yang);
            gameObject.SetActive(false);
        }
    }
    private void Check()
    {

    }
    private void LowSpeed()
    {
        Debug.Log("低速");
        midCount = 0;
        highCount = 0;
        lowCount++;
        if (lowCount >= 18)
        {
            Debug.Log("低速完成");
            lowFinish = true;
        }
    }
    private void MidSpeed()
    {
        Debug.Log("中速");
        lowCount = 0;
        highCount = 0;
        midCount++;
        if (midCount >= 18)
        {
            Debug.Log("中速完成");
            midFinish = true;
        }
    }
    private void HighSpeed()
    {
        Debug.Log("高速");
        midCount = 0;
        lowCount = 0;
        highCount++;
        if (highCount >= 18)
        {
            Debug.Log("高速完成");
            highFinish = true;
        }
    }
    private void PlaySound()
    {
        int index = Random.Range(0, 6);
        gameObject.GetComponent<AudioSource>().clip = clips[index];
        gameObject.GetComponent<AudioSource>().Play();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        inRegion = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        inRegion = false;
    }
}
