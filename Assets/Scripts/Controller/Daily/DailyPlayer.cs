using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyPlayer : MonoBehaviour
{
    public float speed = 5f;
    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
            Move(Vector2.up);
        else if (Input.GetKey(KeyCode.A))
            Move(Vector2.left);
        else if (Input.GetKey(KeyCode.S))
            Move(Vector2.down);
        else if (Input.GetKey(KeyCode.D))
            Move(Vector2.right);
        else
            Move(Vector2.zero);
    }
    private void Move(Vector2 target)
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = target * speed ;
    }
}
