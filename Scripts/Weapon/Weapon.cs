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
    public Vector2 normalScale;

    public Weapon(Joystick joy) 
    {
        joystick = joy;
        weapon = GameObject.Find("ShotStick").transform;
        normalScale = weapon.transform.localScale;
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


        double del = (Math.Sqrt((x * x) + (y * y)));

        if (del != 0) {
            double arcy = Math.Asin(y / del);

            float angle = (float)arcy * 57.32f;

            if (x < 0)
            {
                angle = - angle;
                weapon.localScale = new Vector2(-normalScale.x, normalScale.y);
            }
            else weapon.localScale = new Vector2(normalScale.x, normalScale.y);

            weapon.rotation = Quaternion.Euler(0, 0, angle);

            Debug.Log(angle);
            Debug.Log(arcy);
        }
    }



}
