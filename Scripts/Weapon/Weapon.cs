using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class Weapon : MonoBehaviour
{

    public float bulletSpeed = 5f;
    public Transform weapon;
    public GameObject bullet;
    public Joystick joystick;

    public Weapon(Joystick joy) 
    {
        joystick = joy;
        weapon = GameObject.Find("ShotStick").transform;
    }

    public virtual void shot(Vector2 direction) {}
    public virtual void look() {}



}
public class ShotGun : Weapon
{






    public ShotGun(Joystick joy) : base(joy) 
    { 

    }

    //weapon = gameObject.transform.GetChild(1).transform;





    public override void shot(Vector2 direction)
    {
        Vector2 whereSpawn = new Vector2();
        Instantiate(bullet, transform);

    }

    public override void look()
    {
        //new Vector2(Mathf.Cos((obj.transform.eulerAngles.z + 90f) / 57.32f) * f, Mathf.Sin((obj.transform.eulerAngles.z + 90f) / 57.32f) * f);
        float whereILook;
        double y = joystick.Direction.y;
        double x = joystick.Direction.x;

        double arcy = Math.Asin(y / (Math.Sqrt((x * x) + (y * y))));

        float angle = (float)arcy * 57.32f;


        //weapon.rotation = new (angle);
        weapon.rotation = new Quaternion(0, 0, angle, 1);
        
        Debug.Log(angle);

        //Debug.Log("Asin = ", arcy);


    }



}
