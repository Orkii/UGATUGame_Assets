using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class Weapon : MonoBehaviour {

    public float bulletSpeed = 5f;
    public Transform weapon;
    public GameObject bullet;
    public Joystick joystick;
    public Vector2 normalScale;
    public Vector2 prevJoystick;
    public Vector2 startJoystick;
    public Player parentCode;

    public Weapon(Joystick joy) {
        joystick = joy;
        weapon = GameObject.Find("ShotStick").transform;
        normalScale = weapon.transform.localScale;
        try {
            parentCode = weapon.parent.gameObject.GetComponent<Player>();
        }
        catch { }
        if (parentCode == null) Debug.Log("Weapon parentCode = null");
    }

    public virtual void shot() { }
    public virtual void look() { }

}
public class ShotGun : Weapon {

    const int BULLET_COUNT = 7;
    Rigidbody2D player;
    SpriteRenderer sprite;
    float jumpForce = 10f;



    public ShotGun(Joystick joy) : base(joy) {
        bullet = Resources.Load("Bullet") as GameObject;
        //player = gameObject.GetComponent<Rigidbody2D>();
        player = weapon.parent.GetComponent<Rigidbody2D>();
        sprite = weapon.GetComponent<SpriteRenderer>();
    }
    public override void shot() {
        float angle = weapon.transform.rotation.eulerAngles.z + 180;
        Vector2 boost = new Vector2(Mathf.Cos(angle / 57.32f), Mathf.Sin(angle / 57.32f));

        player.velocity = new Vector2(player.velocity.x + boost.x * jumpForce, player.velocity.y + boost.y * jumpForce);

        for (int i = 0; i < BULLET_COUNT; i++) {
            Instantiate(bullet, weapon.transform.position, weapon.transform.localRotation);
        }
    }
    public void shot(float a) {
        weapon.transform.eulerAngles = new Vector3(0, 0, a);
        Debug.Log(weapon.transform.eulerAngles);

        float angle = weapon.transform.rotation.eulerAngles.z + 180;
        Vector2 boost = new Vector2(Mathf.Cos(angle / 57.32f), Mathf.Sin(angle / 57.32f));

        player.velocity = new Vector2(player.velocity.x + boost.x * jumpForce, player.velocity.y + boost.y * jumpForce);

        for (int i = 0; i < BULLET_COUNT; i++) {
            Instantiate(bullet, weapon.transform.position, weapon.transform.localRotation);
        }
    }

    public override void look() {
        double y = joystick.Direction.y;
        double x = joystick.Direction.x;


        if ((prevJoystick.x != 0) && (prevJoystick.y != 0) && (x == 0) && (y == 0)) {
            if (startJoystick == prevJoystick) {
                if ((parentCode.getIsGround()) || (parentCode.getIsSticky()))
                    parentCode.setJumpInput(1);
                else { 
                    shot(270f);
                    parentCode.setJumpInput(0);
                }
            }      
            else shot();

        }

        if ((prevJoystick.x == 0) && (prevJoystick.y == 0) && (x != 0) && (y != 0)) {
            startJoystick = joystick.Direction;
        }




        prevJoystick = new Vector2((float)x, (float)y);






        double del = (Math.Sqrt((x * x) + (y * y)));

        if (del != 0) {
            double arcy = Math.Asin(y / del);

            float angle = (float)arcy * 57.32f;

            if (x < 0) {
                angle = 180 - angle;
                sprite.flipY = true;
            }
            else
                sprite.flipY = false;

            weapon.rotation = Quaternion.Euler(0, 0, angle);
        }
    }


}
