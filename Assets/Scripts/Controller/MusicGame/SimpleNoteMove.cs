using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleNoteMove : MonoBehaviour
{
    private Rigidbody rb;
    public float speed = 70f;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0,0,-speed);
    }

    void Update()
    {
        if(transform.position.z < -100f){
            Destroy(gameObject);
        }
    }
}
