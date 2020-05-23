using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bling : MonoBehaviour
{
    private Material material;
    private int count = 0;
    public bool finish = false;
    private void Awake()
    {
        material = gameObject.GetComponent<SpriteRenderer>().material;
    }
    private void OnEnable()
    {
        material.SetFloat("_BloomBias", -0.48f);
        count = 0;
    }
    private void OnDisable()
    {
        material.SetFloat("_BloomBias", -1f);
    }
    private void Update()
    {
        if (finish == false)
        {
            count++;
            if (count > 10)
                gameObject.GetComponent<Bling>().enabled = false;
        }
        
    }
}
