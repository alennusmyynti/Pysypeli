using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{ 
    [Header("Thrust Parameters")]
    public float thrustForce = 50f;
    public float breakVelocity = 0.95f;
    //public float maxVelocity = 400f;
    public float maxForceHoldForce = 2f;

    [Header("Walk Parameters")]
    public float moveSpeed = 1f;
    //public bool canMove = true;
    Vector2 moveDir;


    SpriteRenderer spriteRenderer;
    public Color activatedColor;

    Color originalColor;
    Rigidbody2D _rb2D;
    Animator _anime;
    bool _isFiring;
    bool _canLaunch = true;
    private float holdDownStartTimer;

    public PlayerMode mode;
    public PlayerLookDir lookDir = PlayerLookDir.Down;
    //PlayerHandRotator _handrotation;
    /*bool bulletsleft;
    public float firerate = 0.05f;
    private float nextfire = 0.0f;*/
    // Use this for initialization

    void Start()
    {
        _rb2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        _anime = GetComponent<Animator>();
        originalColor = spriteRenderer.color;
    }

    // Update is called once per frame

    void Update()
    {
        //vanha toteutus
       /* if (Input.GetButton("Fire1"))
        {
            Shooting(true);
        }
        else 
        {
            Shooting(false);
        }
        */
        switch(mode)
        {
            case PlayerMode.Walk:
                moveDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
                break;
            case PlayerMode.ThrustMode:
                if (_canLaunch)
                {
                    if (Input.GetButtonDown("Fire1"))
                    {
                        holdDownStartTimer = Time.time;
                    }

                    if (Input.GetButtonUp("Fire1"))
                    {
                        float holdDownTime = Time.time - holdDownStartTimer;
                        LaunchBall(CalculateHoldDownForce(holdDownTime));
                    }
                }
                break;
            case PlayerMode.Attack:

                break;
            case PlayerMode.Die:

                break;
        }
       switch(lookDir)
        {
            case PlayerLookDir.Down:
                if (spriteRenderer.flipX)
                {
                    spriteRenderer.flipX = false;
                }
                break;
            case PlayerLookDir.Left:
                if(!spriteRenderer.flipX)
                {
                    spriteRenderer.flipX = true;
                }
                break;
            case PlayerLookDir.Up:
                if (spriteRenderer.flipX)
                {
                    spriteRenderer.flipX = false;
                }
                break;
            case PlayerLookDir.Right:
                if (spriteRenderer.flipX)
                {
                    spriteRenderer.flipX = false;
                }
                break;
        }
    }

    private float CalculateHoldDownForce(float holdTime)
    {
        float holdTimeNormalized = Mathf.Clamp01(holdTime / maxForceHoldForce);
        float force = holdTimeNormalized * thrustForce;
        return force;
    }

    public void LaunchBall(float force)
    {
        Vector2 positionOnScreen = Camera.main.WorldToViewportPoint(transform.position);
        Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);
        float directionx = positionOnScreen.x - mouseOnScreen.x;
        float directiony = positionOnScreen.y - mouseOnScreen.y;

        Debug.Log($"Force: {force} %");
        _rb2D.AddForce(new Vector2(directionx * force, directiony * force), ForceMode2D.Impulse);
        
    }

    void FixedUpdate()
    {
        //vanha toteutus
        /*
        Vector2 positionOnScreen = Camera.main.WorldToViewportPoint(transform.position);
        Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);

        if (_isFiring)
        {
            float directionx = positionOnScreen.x + mouseOnScreen.x;
            float directiony = positionOnScreen.y - mouseOnScreen.y;
            if (_rb2D.velocity.sqrMagnitude < maxVelocity)
            {
                _rb2D.AddForce(new Vector2(directionx * thrustForce, directiony * thrustForce), ForceMode2D.Impulse);
            }
        }
        else
        {
            //jos haluat jarrutusta
            _rb2D.velocity = _rb2D.velocity * breakVelocity;
        }
        */
        switch(mode)
        {
            case PlayerMode.Walk:
                _rb2D.velocity = moveDir * moveSpeed;
                _anime.SetFloat("MoveX", _rb2D.velocity.x);
                _anime.SetFloat("MoveY", _rb2D.velocity.y);

                if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
                {
                    _anime.SetFloat("LastMoveX", Input.GetAxisRaw("Horizontal"));
                    _anime.SetFloat("LastMoveY", Input.GetAxisRaw("Vertical"));
                }

                break;
            case PlayerMode.ThrustMode:
                if (_rb2D.velocity.sqrMagnitude > 0)
                {
                    //kierre duunaa paremmaksi kun ehdit
                    /*float turn = 250;
                    _rb2D.AddTorque( turn, ForceMode2D.Impulse);*/

                    if (_rb2D.velocity.sqrMagnitude < 0.001)
                    {
                        _rb2D.velocity = Vector2.zero;
                        _canLaunch = true;
                        ChangeColor(originalColor);
                        //spriteRenderer.color = Color.Lerp(spriteRenderer.color, originalColor, 50 * Time.deltaTime);
                    }
                    else
                    {
                        _rb2D.velocity = _rb2D.velocity * breakVelocity;
                        _canLaunch = false;
                        ChangeColor(activatedColor);
                        //spriteRenderer.color = Color.Lerp(spriteRenderer.color, activatedColor, 50 * Time.deltaTime);
                    }

                }
                break;
            case PlayerMode.Attack:

                break;
            case PlayerMode.Die:

                break;
        } 
    }


    public void SetPlayerMode(PlayerMode newMode)
    {
        mode = newMode;
    }

    public void SetPlayerLookDir(PlayerLookDir newLookDir)
    {
        lookDir = newLookDir;
    }


    public void ChangeColor(Color newColor)
    {
        spriteRenderer.color = Color.Lerp(spriteRenderer.color, newColor, 50 * Time.deltaTime);
    }

    public void Shooting(bool b)
    {
        _isFiring = b;
    }
 

    //for magnectic pull force
    public void AddForce(Vector2 force)
    {
        //_rb2D.velocity = Vector2.zero;
        //_rb2D.angularVelocity = 0f;
        _rb2D.AddForce(force);
    }


}
