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

        float rand = UnityEngine.Random.Range(-30f, 30f);
        
        
        gameObject.transform.eulerAngles = new Vector3(0, 0, -gameObject.transform.rotation.eulerAngles.z + rand);

        float angle = gameObject.transform.rotation.eulerAngles.z;

        transform.Rotate(0, 0, 90);


        if (gameObject.transform.localScale.x < 0)
        {
            //angle = -angle;
        }
        //transform.rotation = Quaternion.Euler(0, 0, angle);

        Debug.Log(angle);
        rb.velocity = new Vector2(Mathf.Cos(angle / 57.32f) * bulletSpeed, Mathf.Sin(angle / 57.32f) * bulletSpeed);

    }
    void Update()
    {
        
    }
    
}

