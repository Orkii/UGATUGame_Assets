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

    public virtual void shot() {}
    public virtual void look() {}

}
public class ShotGun : Weapon
{

    int bulletCount = 7;
    Rigidbody2D player;
    float jumpForce = 3f;



    public ShotGun(Joystick joy) : base(joy) 
    {
        bullet = Resources.Load("Bullet") as GameObject;
        //player = gameObject.GetComponent<Rigidbody2D>();
        player = weapon.parent.GetComponent<Rigidbody2D>();
    }
    public override void shot()
    {
        float angle = weapon.transform.rotation.eulerAngles.z + 180;
        Vector2 boost = new Vector2(Mathf.Cos(angle / 57.32f) * bulletSpeed, Mathf.Sin(angle / 57.32f) * bulletSpeed);

        player.velocity = new Vector2(player.velocity.x + boost.x * jumpForce, player.velocity.x + boost.y * jumpForce);

        for (int i = 0; i < bulletCount; i++)
        {
            Instantiate(bullet, weapon.transform.position, weapon.transform.localRotation);
        }
    }

    public override void look()
    {
        double y = joystick.Direction.y;
        double x = joystick.Direction.x;

        if ((x == 0) && (y==0) && (prevJoystick.x != 0) && (prevJoystick.y != 0))
        {
            shot();
        }

        prevJoystick = new Vector2 ((float)x, (float)y);

        double del = (Math.Sqrt((x * x) + (y * y)));

        if (del != 0) {
            double arcy = Math.Asin(y / del);

            float angle = (float)arcy * 57.32f;

            if (x < 0)
            {
                angle = 180 - angle;
                //weapon.localScale = new Vector2(normalScale.x, -normalScale.y);
                weapon.GetComponent<SpriteRenderer>().flipY = true;
            }
            else weapon.GetComponent<SpriteRenderer>().flipY = false;
            //weapon.localScale = new Vector2(normalScale.x, normalScale.y);

            weapon.rotation = Quaternion.Euler(0, 0, angle);
        }
    }


}
