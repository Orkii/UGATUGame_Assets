using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//using Random = UnityEngine.Random;


public class Enemy : Player
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
    override public void death() { }


    override public void respawn() { }


    public int i = 0;
    float jumpInput = 0;
    float timePreviousJumpButton = 0;
    public List<GameObject> areasEnemy = new List<GameObject>();


    float sign = 0;
    float prevSign = 0;
    void FixedUpdate()
    {
        sign = Math.Sign(areasEnemy[i].transform.position.x - transform.position.x);

        if (prevSign != sign)
        {

            i++;
            if (i >= areasEnemy.Count) i = 0;

            sign = Math.Sign(areasEnemy[i].transform.position.x - gameObject.transform.position.x);
        }
        prevSign = sign;




        moveX(sign);

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
        jump(jumpInput, timePreviousJumpButton);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Sticky")
        {
            setIsSticky(true);
            setStickyPos(col.GetContact(1).point.x);
        }
    }
    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Sticky")
        {
            setIsSticky(false);
        }
    }
}