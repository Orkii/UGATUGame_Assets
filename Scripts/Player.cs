using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class Character
{
    public GameObject gameObject;
    public Rigidbody2D rigidbody2D;
    ParticleSystem particleSystem;
    GameObject canvas;
    const float MAX_X_SPEED_NORMAL = 5f;
    const float MAX_X_SPEED_STICKY = 15f;
    const float ACCELERATION = 10f;
    const float JUMP_FORCE = 10f;
    //const float STICKY_BOOST = 3f;
    const float STICKY_SPEED_DOWN = -3f;
    const float STICKY_JUMP_BOOST_X = 5f;
    const float STICKY_JUMP_BOOST_Y = 10f;
    const float DECELERATION = 12f;
    const int MORE_JUMP_COUNT = 1; 
    public bool isGround = false;
    bool isSticky = false;
    float stickyPos = 0;
    float timePreviousJump = 0;
    int moreJump = MORE_JUMP_COUNT;
    const float MAX_X_SPEED = MAX_X_SPEED_STICKY;
    float moveXKey = 0;
    float scaleX;

    float moveX_X;
    float moveX_X4;

    float NORMOLIZE_SPEED_X; 


    public Character(GameObject it)
    {
        gameObject = it;
        rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        particleSystem = gameObject.GetComponent<ParticleSystem>();
        canvas = GameObject.Find("Canvas");
        scaleX = gameObject.transform.localScale.x;

        moveX_X = ACCELERATION * Time.fixedDeltaTime;
        moveX_X4 = moveX_X * 4;

        NORMOLIZE_SPEED_X = DECELERATION * Time.fixedDeltaTime;
    }
    public void setIsGround(bool b)
    {
        isGround = b;
    }
    public void setStickyPos(float b)
    {
        stickyPos = b;
    }
    public void setIsSticky(bool b)
    {
        isSticky = b;
    }
    public float getMAX_X_SPEED_STICKY()
    {
        return (MAX_X_SPEED_STICKY);
    }
    public float getMAX_X_SPEED_NORMAL()
    {
        return (MAX_X_SPEED_NORMAL);
    }
    public bool getIsGround()
    {
        return isGround;
    }
    public void setMAX_X_SPEED(float b)
    {
        //MAX_X_SPEED = b;
    }
    public void normolizeSpeed()
    {
        if (Math.Abs(rigidbody2D.velocity.x) > MAX_X_SPEED_NORMAL)
        {
            if (rigidbody2D.velocity.x > 0)//++
            {

                if (rigidbody2D.velocity.x > MAX_X_SPEED) rigidbody2D.velocity = new Vector2(MAX_X_SPEED, rigidbody2D.velocity.y);// Много скорости
               
                if (isSticky == false) rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x - DECELERATION * Time.fixedDeltaTime, rigidbody2D.velocity.y);
                

            }
            else//--
            {

                if (rigidbody2D.velocity.x < -MAX_X_SPEED) rigidbody2D.velocity = new Vector2(-MAX_X_SPEED, rigidbody2D.velocity.y);

                if (isSticky == false) rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x + DECELERATION * Time.fixedDeltaTime, rigidbody2D.velocity.y);
                
            }   
        }
      
        
        if ((moveXKey == 0))
        {
            if (rigidbody2D.velocity.x > 0.1)
            {
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x - NORMOLIZE_SPEED_X, rigidbody2D.velocity.y);
            }
            else if(rigidbody2D.velocity.x < -0.1)
            {
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x + NORMOLIZE_SPEED_X, rigidbody2D.velocity.y);
            }

            if ((-0.2f < rigidbody2D.velocity.x) && (rigidbody2D.velocity.x < 0.2f))
            {
                rigidbody2D.velocity = new Vector2(0f, rigidbody2D.velocity.y);
            }


        }


        if ((isSticky == true) && (isGround == false))
        {
            if (rigidbody2D.velocity.y < STICKY_SPEED_DOWN)
            {
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, STICKY_SPEED_DOWN);
            }
        }
    }
    public void moveX(float direction)
    {

        moveXKey = direction;

        if (direction > 0)
        {
            
            if (Math.Sign(rigidbody2D.velocity.x) != direction)
            {
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x + moveX_X4, rigidbody2D.velocity.y);
            }
            else
            {
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x + moveX_X, rigidbody2D.velocity.y);
            }
        }
        else if (direction < 0)
        {

            if (Math.Sign(rigidbody2D.velocity.x) != direction)
            {
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x - moveX_X4, rigidbody2D.velocity.y);
            }
            else
            {
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x - moveX_X, rigidbody2D.velocity.y);
            }
        }



        normolizeSpeed();
        flip();
    }
    public void flip()
    {
        if(rigidbody2D.velocity.x > 0)
            gameObject.transform.localScale = new Vector3(scaleX, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
        else if (rigidbody2D.velocity.x < 0)
            gameObject.transform.localScale = new Vector3(-scaleX, gameObject.transform.localScale.y, gameObject.transform.localScale.z);

        //gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
    }
    public void jump(float jumpInput, float timePreviousJumpButton)
    {
        timePreviousJump+= timePreviousJumpButton;

        if (jumpInput > 0)
        {
            if (timePreviousJump > 0.1f)
            {
                if (isGround == true)
                {
                    rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, JUMP_FORCE);
                    moreJump = MORE_JUMP_COUNT;
                }
                else if (isSticky == true)
                {
                    if (forHoney() > 0)
                    {
                        rigidbody2D.velocity = new Vector2(-STICKY_JUMP_BOOST_X, JUMP_FORCE);
                    }
                    else if (forHoney() < 0)
                    {
                        rigidbody2D.velocity = new Vector2(STICKY_JUMP_BOOST_X, JUMP_FORCE);
                    }
                    moreJump = MORE_JUMP_COUNT;
                }
                else if (moreJump != 0)
                {
                    rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, JUMP_FORCE);
                    moreJump--;
                    
                }
                timePreviousJump = 0;
                
            }
        }
    }
    float forHoney()
    {
        if ((isSticky == true) && (isGround == false))
        {
            //Debug.Log("Touch pos " + (gameObject.transform.position.x - stickyPos));

            if     ((gameObject.transform.position.x - stickyPos) > 0) return -1;
            else if((gameObject.transform.position.x - stickyPos) < 0) return 1;
        }
        return 0;
    }
    public void doSplash()
    {
        particleSystem.Play();
    }
    public void death()
    {
        respawn();
    }
    public void respawn()
    {
        GameObject spawnPoint = GameObject.FindGameObjectWithTag("Respawn");
        gameObject.transform.position = spawnPoint.transform.position;
    }
    public void win()
    {
        GameObject win;
        win = canvas.transform.Find("Win").gameObject;
        win.SetActive(true);
    }
}


public class Player : MonoBehaviour
{



    Character character;
    private Rigidbody2D rb;
    float timePreviousJumpButton = 0;
    public Character getCharacter()
    {
        return character;
    }

    void Start()
    {
        character = new Character(gameObject);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        float jumpInput = Input.GetAxisRaw("Jump");

        
        if (jumpInput == 1)
        {
            timePreviousJumpButton = 0f;
        }
        timePreviousJumpButton += Time.fixedDeltaTime;
        character.jump(jumpInput, timePreviousJumpButton);
        character.moveX(moveInput.x);

    }
    void OnTriggerStay2D(Collider2D col)
    {               //если в тригере что то есть и у обьекта тег "ground"

        if (col.tag == "Floor") 
        {
            character.setIsSticky(false);
            if (character.getIsGround() == false) character.doSplash();
            character.setIsGround(true);
        }      
        else if (col.tag == "Death") character.death();
        else if (col.tag == "Finish") character.win();
    }
    void OnTriggerExit2D(Collider2D col)
    {              //если из триггера что то вышло и у обьекта тег "ground"
        if (col.tag == "Floor") character.setIsGround(false);     //то вЫключаем переменную "на земле"

    }
    void OnCollisionStay2D(Collision2D col)
    {
        if (character.getIsGround() == false)
        {
            if (col.gameObject.tag == "Floor")
            {
                character.setIsSticky(true);
                character.setStickyPos(col.GetContact(1).point.x);
            }
        }

        if (col.gameObject.tag == "Death")
        {
            character.death();
        }
        else if (col.gameObject.tag == "Enemy")
        {
            character.death();
        }
    }
    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Floor")
        {
            character.setIsSticky(false);
        }   
    }
}
