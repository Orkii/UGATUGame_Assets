using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//using Random = UnityEngine.Random;


public class Enemy : Player
{
    bool trigerRight = false;

    public void setTriggerRight(bool b)
    {
        trigerRight = b;
    }
    override public void death() 
    {
        Debug.Log("Death");
        Destroy(gameObject);
    }
    override public void respawn() { }

    override public void flip() 
    {
        if (rigidbody2D.velocity.x > 0)
            gameObject.transform.localScale = new Vector3(scaleX, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
        else if (rigidbody2D.velocity.x < 0)
            gameObject.transform.localScale = new Vector3(-scaleX, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
    }



    public int i = 0;
    float jumpInput = 0;
    float timePreviousJumpButton = 0;
    public List<GameObject> areasEnemy = new List<GameObject>();


    float sign = 0;
    float prevSign = 0;

    void Update()
    {

    }
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




        

        if (trigerRight == true)
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
        moveX(sign);
    }


    void OnCollisionStay2D() { }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Sticky")
        {
            setIsSticky(true);
            setStickyPos(col.GetContact(1).point.x);
        }

    }
    public override void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Floor")
        {
            setIsSticky(false);
            if (!roof)
            {
                setMoreJump(MORE_JUMP_COUNT);
            }
        }
    }

    public override void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Bullet")
        {
            Debug.Log("I dead");
            death();

        }
    }
    public override void OnTriggerExit2D(Collider2D col)
    {

    }
    
}