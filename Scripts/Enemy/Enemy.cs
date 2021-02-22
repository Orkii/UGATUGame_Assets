using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//using Random = UnityEngine.Random;


public class Enemy : Player {
    bool trigerRight = false;

    public void setTriggerRight(bool b) {
        trigerRight = b;
    }
    override public void respawn() { }

    override public void flip() {
        if (rigidbody2D.velocity.x > 0)
            gameObject.transform.localScale = new Vector3(scaleX, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
        else if (rigidbody2D.velocity.x < 0)
            gameObject.transform.localScale = new Vector3(-scaleX, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
    }



    public int i = 0;
    private float jumpInpute = 0;
    private float timePreviousJumpButtone = 0;
    public List<GameObject> areasEnemy = new List<GameObject>();

    public GameObject dedPart;



    float sign = 0;
    float prevSign = 0;


    void Update() {
        sign = Math.Sign(areasEnemy[i].transform.position.x - transform.position.x);

        if (prevSign != sign) {
            i++;
            if (i >= areasEnemy.Count)
                i = 0;
            sign = Math.Sign(areasEnemy[i].transform.position.x - gameObject.transform.position.x);
        }
        prevSign = sign;

        if (trigerRight == true) {
            jumpInpute = 1;
        }
        else jumpInpute = 0;
        Debug.Log(jumpInpute);
    }
    void FixedUpdate() {
        if (jumpInpute == 1) {
            timePreviousJumpButtone = 0f;
        }

        timePreviousJumpButtone += Time.fixedDeltaTime;
        jump(jumpInpute, timePreviousJumpButtone);
        moveX(sign);
    }

    public override void OnCollisionStay2D(Collision2D col) {
        if (getIsGround() == false) {
            if (col.gameObject.tag == "Floor") {
                setIsSticky(true);
                setStickyPos(col.GetContact(1).point.x); //как работает?
                ///
                // if (moveInput.x == 0)
                //   setIsSticky(false);
            }
        }
    }
    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag == "Sticky") {
            setIsSticky(true);
            setStickyPos(col.GetContact(1).point.x);
        }
    }
    public override void OnCollisionExit2D(Collision2D col) {
        if (col.gameObject.tag == "Floor") {
            setIsSticky(false);
            if (!roof) {
                setMoreJump(MORE_JUMP_COUNT);
            }
        }
    }

    public override void OnTriggerStay2D(Collider2D col) {
        if (col.gameObject.tag == "Bullet") {
            Destroy(col.gameObject);
            destroy();
        }
    }
    public override void OnTriggerExit2D(Collider2D col) {

    }

    public void destroy() {
        Debug.Log("Enemy Помер");
        GameObject a = Instantiate(dedPart, transform.position, Quaternion.identity);
        Destroy(a, 0.5f);
        Destroy(gameObject);
    }



}