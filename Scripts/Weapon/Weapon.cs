using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class Weapon : MonoBehaviour
{

    public float bulletSpeed = 5f;
    public Transform parent;
    public GameObject bullet;
    public Joystick joystick;

    public Weapon(Joystick joy) 
    {
        joystick = joy;
    }

    public virtual void shot(Vector2 direction) {}
    public virtual void look(Vector2 direction) {}



}
public class ShotGun : Weapon
{

    public ShotGun(Joystick joy) : base(joy)
    {

    }



    override public void shot(Vector2 direction)
    {
        Vector2 whereSpawn = new Vector2();
        Instantiate(bullet, transform);

    }

    override public void look(Vector2 direction)
    {
        //new Vector2(Mathf.Cos((obj.transform.eulerAngles.z + 90f) / 57.32f) * f, Mathf.Sin((obj.transform.eulerAngles.z + 90f) / 57.32f) * f);
        float whereILook;
        double y = joystick.Direction.y;
        double x = joystick.Direction.x;

        double arcy = Math.Asin(y / (Math.Sqrt((x * x) + (y * y))));

        //Debug.Log("Asin = ", arcy);


    }



}
