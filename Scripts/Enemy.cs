using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//using Random = UnityEngine.Random;


public class Enemy : MonoBehaviour
{
    bool trigerRight = false;
    bool trigerLeft = false;

    public void setTriggerLeft(bool b)
    {
        trigerLeft = b;
    }
    public void setTriggerRight(bool b)
    {
        trigerRight = b;
    }
    public void setIsGround(bool b)
    {
        enemy.setIsGround(b);
    }



    public int i;
    float jumpInput = 0;
    float timePreviousJumpButton = 0;
    public Character enemy;
    public List<GameObject> areasEnemy = new List<GameObject>();

    void Start()
    {
        i = 0;
        enemy = new Character(gameObject);
    }
    float sign = 0;
    float prevSign = 0;
    void FixedUpdate()
    {

        //Debug.Log("areasEnemy = " + areasEnemy.Count + "   i = " + i + "     Sign = " + sign + "   prevSign = " + prevSign);
        sign = Math.Sign(areasEnemy[i].transform.position.x - enemy.gameObject.transform.position.x);

        if (prevSign != sign)
        {

            i++;
            if (i >= areasEnemy.Count) i = 0;

            sign = Math.Sign(areasEnemy[i].transform.position.x - enemy.gameObject.transform.position.x);
        }
        prevSign = sign;




        enemy.moveX(sign);

        if ((trigerRight == true) || (trigerLeft == true))
        {
            jumpInput = 1;
        }
        else jumpInput = 0; 

        if (jumpInput == 1)
        {
            timePreviousJumpButton = 0f;
        }
        timePreviousJumpButton += Time.fixedDeltaTime;
        enemy.jump(jumpInput, timePreviousJumpButton);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Sticky")
        {
            enemy.setIsSticky(true);
            enemy.setStickyPos(col.GetContact(1).point.x);
        }
    }
    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Sticky")
        {
            enemy.setIsSticky(false);
        }
    }
}