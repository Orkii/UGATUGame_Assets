﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;

public class Bullet : MonoBehaviour {
    //public Joystick joystick;
    float bulletSpeed = 5f;
    Rigidbody2D rb;
    Transform tr;
    new ParticleSystem particleSystem;
    new CapsuleCollider2D collider;

    void Start() {
        //joystick = FindObjectOfType<Joystick>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        particleSystem = gameObject.GetComponent<ParticleSystem>();
        collider = GetComponent<CapsuleCollider2D>();
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, - 1);







        tr = gameObject.transform;

        float rand = UnityEngine.Random.Range(-30f, 30f);


        gameObject.transform.eulerAngles = new Vector3(0, 0, -gameObject.transform.rotation.eulerAngles.z + rand);

        float angle = gameObject.transform.rotation.eulerAngles.z;

        transform.Rotate(0, 0, 90);


        if (gameObject.transform.localScale.x < 0) {
            //angle = -angle;
        }
        //transform.rotation = Quaternion.Euler(0, 0, angle);
        rb.velocity = new Vector2(Mathf.Cos(angle / 57.32f) * bulletSpeed, Mathf.Sin(angle / 57.32f) * bulletSpeed);

    }

    void OnTriggerStay2D(Collider2D col) {

        if (col.gameObject.tag == "Floor") {
           
            
            
            
            
            destroy();
        }
        else if (col.gameObject.tag == "Enemy") {
            
          
            col.gameObject.GetComponent<Enemy>().destroy();
            destroy();
        }
    }
    public void destroy() {
        GetComponent<ParticleSystem>().Play();
        gameObject.GetComponent<Renderer>().enabled = false;
        collider.enabled = false;
        rb.velocity = new Vector2(0, 0);
        gameObject.GetComponent<Light>().enabled = false;

        Invoke("fullDestoy", 1);
    }
    void fullDestoy() {
        Destroy(gameObject);
    }


}

