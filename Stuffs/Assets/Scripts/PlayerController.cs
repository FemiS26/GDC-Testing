using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpSpeed = 8f;
    private float direction = 0f;
    private Rigidbody2D player;
    public Animator anim;
    private String currentState;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    private bool isTouchingGround;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        isTouchingGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        direction = Input.GetAxis("Horizontal");

        if (direction > 0f)
        {
            player.velocity = new Vector2(direction * speed, player.velocity.y);
        }
        else if (direction < 0f){
            player.velocity = new Vector2(direction * speed, player.velocity.y);
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
            ChangeState("HeroKnight_Jump");
        }
        else if (player.velocity.y < 0)
        {
            ChangeState("HeroKnight_Fall");
        }

        if((player.velocity.x >= 0.1f || player.velocity.x <= -0.1f) && player.velocity.y == 0)
        {
            
            ChangeState("HeroKnight_Run");
        }
        else if(player.velocity.y == 0)
        {
            ChangeState("HeroKnight_Idle");
        }

        SpriteRenderer SpriteCranberry;
            SpriteCranberry = GetComponent<SpriteRenderer>();
            SpriteCranberry.flipX = player.velocity.x < 0;
    }

    private void ChangeState(String newState)
    {
        if(currentState != newState)
        {
            currentState = newState;
            anim.Play(newState);
        }
    }
}