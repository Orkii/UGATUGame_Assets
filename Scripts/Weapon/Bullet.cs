using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bullet : MonoBehaviour
{
    //public Joystick joystick;
    float bulletSpeed = 5f;
    Rigidbody2D rb;
    Transform tr;
    void Start()
    {
        //joystick = FindObjectOfType<Joystick>();
        rb = GetComponent<Rigidbody2D>();
        tr = gameObject.transform;
        float angle = gameObject.transform.rotation.z;

        if (gameObject.transform.localScale.x < 0)
        {
            angle = -angle;
        }
        transform.rotation = Quaternion.Euler(0, 0, angle);

        rb.velocity = new Vector2(Mathf.Cos(angle / 57.32f) * bulletSpeed, Mathf.Sin(angle / 57.32f) * bulletSpeed);
        Debug.Log(rb.velocity);

    }
    void Update()
    {
        
    }
}

