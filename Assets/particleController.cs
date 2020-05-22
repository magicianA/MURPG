using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class particleController : MonoBehaviour
{
    public ParticleSystem[] hit;
    private int index = 0;
    private float timer = 0f;
    private bool isNext = true;

    // Start is called before the first frame update
    private void Start()
    {
        hit = gameObject.GetComponent<ParticleSystem[]>();
    }

    // Update is called once per frame
    private void Update()
    {
        float temp = timer;
        timer += Time.fixedDeltaTime;
        index = (int)(timer / 2f) % 3;
        int index01 = (int)(temp / 2f) % 3;
        if (index == index01)
        {
            isNext = false;
        }
        else
        {
            isNext = true;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            for (int i = 0; i < hit.Length; i++)
            {
                hit[i].Play();
            }
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            hit[index].Play();
        }
    }

    public static IEnumerator DelayFuc(Action action, float delaySeconds)
    {
        action();
        yield return new WaitForSeconds(delaySeconds);
    }
}