using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    [SerializeField] private Animator anim;
    private int combo = 1;
    [SerializeField] private float swingDelay;
    private float swingProg;
    [SerializeField] private PlayerController PC;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
            if(Input.GetButtonDown("Attack") && swingProg == 0)
        {
            swingProg = swingDelay;
            if(combo == 1)
            {
                StartCoroutine(PC.ChangeState("HeroKnight_Attack1", true, swingDelay));
                StartCoroutine(PC.stopPlayer(swingDelay));
                combo++;
            }
            else if(combo == 2)
            {
                StartCoroutine(PC.ChangeState("HeroKnight_Attack2", true, swingDelay));
                StartCoroutine(PC.stopPlayer(swingDelay));
                combo++;
            }
            else
            {
                StartCoroutine(PC.ChangeState("HeroKnight_Attack3", true, swingDelay));
                StartCoroutine(PC.stopPlayer(swingDelay));
                combo = 1;
            }
        }
            swingProg = math.clamp(swingProg - Time.deltaTime, 0, swingDelay);
    }
}
