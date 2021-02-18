using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;






public class Player : MonoBehaviour
{
    const float fixedDeltaTime = 0.02f;
    GameObject canvas;
    const float MAX_X_SPEED_NORMAL = 5f;
    const float MAX_X_SPEED_STICKY = 15f;
    const float ACCELERATION = 10f;
    const float JUMP_FORCE = 10f;
    //const float STICKY_BOOST = 3f;
    const float STICKY_SPEED_DOWN = -3f;
    const float STICKY_JUMP_BOOST_X = 5f;
    const float DECELERATION = 12f;
    public const int MORE_JUMP_COUNT = 1;
    public bool isGround = false;
    bool isSticky = false;
    float stickyPos = 0;
    float timePreviousJump = 0;

    int moreJump = MORE_JUMP_COUNT;
    const float MAX_X_SPEED = MAX_X_SPEED_STICKY;
    float moveXKey = 0;
    public float scaleX;

    public bool roof = false;
    float moveX_X = ACCELERATION * fixedDeltaTime;

    float moveX_X4 = ACCELERATION * fixedDeltaTime * 4;

    float NORMOLIZE_SPEED_X = DECELERATION * fixedDeltaTime;
    
    /*
    public (GameObject it)
    {
        gameObject = it;
        rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        particleSystem = gameObject.GetComponent<ParticleSystem>();
        canvas = GameObject.Find("Canvas");
        scaleX = gameObject.transform.localScale.x;

        
        

        NORMOLIZE_SPEED_X = DECELERATION * Time.fixedDeltaTime;



    }
    */
    public void setRoof(bool b)
    {
        roof = b;
    }// Для определения потолка // Для Head


    public void setIsGround(bool b)
    {
        isGround = b;
    }// Для определения пола // 
    public void setStickyPos(float b)
    {
        stickyPos = b;
    }// Для расположения Стен //
    public void setIsSticky(bool b)
    {
        isSticky = b;
    }// Для Стен //
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
    public void setMoreJump(int b)
    {
        moreJump = b;
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
            else if (rigidbody2D.velocity.x < -0.1)
            {
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x + NORMOLIZE_SPEED_X, rigidbody2D.velocity.y);
            }

            if ((-0.2f < rigidbody2D.velocity.x) && (rigidbody2D.velocity.x < 0.2f))
            {
                rigidbody2D.velocity = new Vector2(0f, rigidbody2D.velocity.y);
            }


        }
    }// Замедление //
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

        if (isSticky == true)
        {
            if (direction != 0)
            {
                if (forHoney() == direction)
                {
                    if (rigidbody2D.velocity.y < STICKY_SPEED_DOWN) rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, STICKY_SPEED_DOWN);
                }
            }
        }


        normolizeSpeed();
        flip();
    }// Движение // 
    public virtual void flip()
    {
        if (rigidbody2D.velocity.x > 0) spriteRenderer.flipX = false;
        //gameObject.transform.localScale = new Vector3(scaleX, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
        else if (rigidbody2D.velocity.x < 0) spriteRenderer.flipX = true;
        //gameObject.transform.localScale = new Vector3(-scaleX, gameObject.transform.localScale.y, gameObject.transform.localScale.z);


    }// Поворот // 
    public void jump(float jumpInput, float timePreviousJumpButton)
    {
        timePreviousJump += timePreviousJumpButton;

        if (jumpInput > 0)
        {
            if (timePreviousJump > 0.1f)
            {
                if (!roof)
                {
                    if (isGround)
                    {
                        rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, JUMP_FORCE);

                        audioSource.PlayOneShot(soundJump);

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
                        audioSource.PlayOneShot(soundJump);
                        moreJump = MORE_JUMP_COUNT;
                    }
                    else if (moreJump != 0)
                    {
                        rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, JUMP_FORCE);
                        audioSource.PlayOneShot(soundJump);
                        moreJump--;

                    }
                }
                timePreviousJump = 0;

            }
        }
    }// Прыжок //
    float forHoney()
    {
        if ((isSticky == true) && (isGround == false))
        {
            //Debug.Log("Touch pos " + (gameObject.transform.position.x - stickyPos));

            if ((gameObject.transform.position.x - stickyPos) > 0) return -1;
            else if ((gameObject.transform.position.x - stickyPos) < 0) return 1;
        }
        return 0;
    }// Где стена? // -1Слева 1Справа
    public void doSplash()
    {
        particleSystem.Play();
    }
    public virtual void death()
    {
        respawn();
    }
    public virtual void respawn()
    {
        GameObject spawnPoint = GameObject.FindGameObjectWithTag("Respawn");
        gameObject.transform.position = spawnPoint.transform.position;
    }
    public  void win()
    {
        GameObject win;
        win = canvas.transform.Find("Win").gameObject;
        win.SetActive(true);
    }



    private Rigidbody2D rb;
    float timePreviousJumpButton = 0;


    AudioSource audioSource;
    public AudioClip soundJump;
    public AudioClip soundFall;
    public new Rigidbody2D rigidbody2D;
    new ParticleSystem particleSystem;
    SpriteRenderer spriteRenderer;


    Weapon arm;
    Joystick joystick;


    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        particleSystem = GetComponent<ParticleSystem>();
        audioSource = GetComponent<AudioSource>();
        scaleX = gameObject.transform.localScale.x;
        joystick = FindObjectOfType<Joystick>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        arm = new ShotGun(joystick);
    }
    float jumpInput = 0;
    Vector2 moveInput;
    void Update()
    {
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (Input.GetKeyDown("space")) jumpInput = 1;
        if (Input.GetKeyUp("space")) jumpInput = 0;


        if (jumpInput == 1)
        {
            timePreviousJumpButton = 0f;
        }
        timePreviousJumpButton += Time.deltaTime;

        arm.look();

    }

    void FixedUpdate()
    {
        jump(jumpInput, timePreviousJumpButton);
        moveX(moveInput.x);
    }
    public virtual void OnTriggerStay2D(Collider2D col)
    {               //если в тригере что то есть и у обьекта тег "ground"

        if (col.tag == "Floor")
        {
            if (!roof)
            {
                setIsSticky(false);
                if (getIsGround() == false) 
                {
                    doSplash();
                    audioSource.PlayOneShot(soundFall);
                }
                setIsGround(true);
            }
        }      
        else if (col.tag == "Death") death();
        else if (col.tag == "Finish") win();
    }
    public virtual void OnTriggerExit2D(Collider2D col)
    {              //если из триггера что то вышло и у обьекта тег "ground"
        if (col.tag == "Floor") setIsGround(false);     //то вЫключаем переменную "на земле"

    }
    public virtual void OnCollisionStay2D(Collision2D col)
    {
        if (getIsGround() == false)
        {
            if (col.gameObject.tag == "Floor")
            {
                setIsSticky(true);
                setStickyPos(col.GetContact(1).point.x); //как работает?
                ///
               // if (moveInput.x == 0)
                 //   setIsSticky(false);
            }
        }

        if (col.gameObject.tag == "Death")
        {
            Debug.Log("I dead Death");
            death();
        }
        else if (col.gameObject.tag == "Enemy")
        {
            Debug.Log("I dead Enemy");
            death();
        }
    }
    public virtual void OnCollisionExit2D(Collision2D col)
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
    public void PlayAudio(AudioClip clip)
    {
        GetComponent<AudioSource>().PlayOneShot(clip);
    }


    
}
