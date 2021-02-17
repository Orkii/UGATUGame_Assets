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
    public Vector2 prevJoystick;
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
        bullet = Resources.Load("Bullet") as GameObject;
    }

    //weapon = gameObject.transform.GetChild(1).transform;





    public override void shot(Vector2 direction)
    {
        //Vector3 whereSpawn = new Vector3(direction.x, direction.y, 0);


        Debug.Log(weapon.transform.position);
        //Instantiate(bullet, weapon.transform);
        Instantiate(bullet, weapon.transform.position, Quaternion.Euler(weapon.localRotation.x, weapon.localRotation.y, weapon.localRotation.z));

    }

    public override void look()
    {
        //new Vector2(Mathf.Cos((obj.transform.eulerAngles.z + 90f) / 57.32f) * f, Mathf.Sin((obj.transform.eulerAngles.z + 90f) / 57.32f) * f);



        //float whereILook;
        double y = joystick.Direction.y;
        double x = joystick.Direction.x;

        if ((x == 0) && (y==0) && (prevJoystick.x != 0) && (prevJoystick.y != 0))
        {
            shot(prevJoystick);
        }


        prevJoystick = new Vector2 ((float)x, (float)y);
        

        double del = (Math.Sqrt((x * x) + (y * y)));

        if (del != 0) {
            double arcy = Math.Asin(y / del);

            float angle = (float)arcy * 57.32f;

            if (x < 0)
            {
                angle = 180 - angle;
                weapon.localScale = new Vector2(normalScale.x, -normalScale.y);
            }
            else weapon.localScale = new Vector2(normalScale.x, normalScale.y);

            weapon.rotation = Quaternion.Euler(0, 0, angle);
        }

    }



}
