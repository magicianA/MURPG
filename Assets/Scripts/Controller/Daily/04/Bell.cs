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

    public GameObject purple;
    public GameObject yellow;
    public GameObject green;
    private void Start()
    {
        backpack = GameObject.FindWithTag("Backpack").GetComponent<Backpack>();
        yang = new InteractiveItem("Daily/道具/洋", "洋");
        lowCount = 0;
        midCount = 0;
        highCount = 0;
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
        green.GetComponent<Bling>().enabled = true;
        lowCount++;
        if (lowCount >= 18)
        {
            Debug.Log("低速完成");
            lowFinish = true;
            green.GetComponent<Bling>().finish = true;

        }
    }
    private void MidSpeed()
    {
        Debug.Log("中速");
        yellow.GetComponent<Bling>().enabled = true;
        midCount++;
        if (midCount >= 18)
        {
            Debug.Log("中速完成");
            midFinish = true;
            yellow.GetComponent<Bling>().finish = true;

        }
    }
    private void HighSpeed()
    {
        purple.GetComponent<Bling>().enabled = true;
        Debug.Log("高速");
        highCount++;
        if (highCount >= 18)
        {
            Debug.Log("高速完成");
            highFinish = true;
            purple.GetComponent<Bling>().finish = true;
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
