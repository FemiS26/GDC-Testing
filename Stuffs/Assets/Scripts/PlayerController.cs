using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]private float speed;
    [SerializeField]private float jumpSpeed;
    private float direction = 0f;
    private Rigidbody2D player;
    public Animator anim;
    private String currentState;
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    private bool isTouchingGround;
    private bool facingRight;
    private bool stateFreeze;
    private float currentSpeed;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        currentSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        isTouchingGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        direction = Input.GetAxis("Horizontal");

        if (direction > 0f)
        {
            facingRight = false;
            player.velocity = new Vector2(direction * currentSpeed, player.velocity.y);
        }
        else if (direction < 0f){
            facingRight = true;
            player.velocity = new Vector2(direction * currentSpeed, player.velocity.y);
        }
        else
        {
            player.velocity = new Vector2(0, player.velocity.y);
        }

        if(Input.GetButton("Jump") && isTouchingGround)
        {
            player.velocity = new Vector2(player.velocity.x, jumpSpeed);
        }

        if(player.velocity.y > 0)
        {
            StartCoroutine (ChangeState("HeroKnight_Jump", false, 0));
        }
        else if (player.velocity.y < 0)
        {
            StartCoroutine (ChangeState("HeroKnight_Fall", false, 0));
        }

        if((player.velocity.x >= 0.1f || player.velocity.x <= -0.1f) && player.velocity.y == 0)
        {
            
            StartCoroutine (ChangeState("HeroKnight_Run", false, 0));
        }
        else if(player.velocity.y == 0)
        {
            StartCoroutine (ChangeState("HeroKnight_Idle", false, 0));
        }

        SpriteRenderer SpriteCranberry;
            SpriteCranberry = GetComponent<SpriteRenderer>();
            SpriteCranberry.flipX = facingRight;
    }

    public IEnumerator ChangeState(String newState, bool disable, float duration)
    {
        if (currentState != newState && stateFreeze == false)
        {
            if (disable == true)
            {
                stateFreeze = true;
            }
            currentState = newState;
            anim.Play(newState);
        }

        if (disable == true)
        {
            yield return new WaitForSeconds (duration);
            stateFreeze = false;
        }
    }
    public IEnumerator stopPlayer(float duration)
    {
        currentSpeed = speed / 4;
        yield return new WaitForSeconds(duration);
        currentSpeed = speed;
    }
}