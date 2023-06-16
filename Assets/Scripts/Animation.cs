using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Animation : MonoBehaviour
{
    [SerializeField] GameObject averyFront;
    [SerializeField] GameObject averyRight;
    [SerializeField] GameObject averyLeft;
    [SerializeField] GameObject averyBack;
    [SerializeField] GameObject grimoire;
    [SerializeField] GameObject player; public GameObject Player { get => player; }
    [SerializeField] GameObject companion; public GameObject Companion { get => companion; }

    GameObject weapon;
    Animator anim;
    Animator anim2;
    Animator anim3;
    string currentState;
    bool walk;
    bool playerAttack;

    [System.Obsolete]
    private void Start()
    {
        weapon = player.transform.FindChild("WeaponObj").gameObject;
    }

    public void PlayerMoveAnimate()
    {
        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
        Rigidbody rb = playerMovement.Rb;
        float horizontalInput = playerMovement.HorizontalInput;
        float verticalInput = playerMovement.VerticalInput;

        //Animation Player
        if (rb.velocity.magnitude > 1f && playerAttack == false)
        {
            if (verticalInput < 0)
            {
                if (weapon.transform.childCount > 0)
                {
                    anim = weapon.GetComponentInChildren<Animator>();
                    ChangeAnimationState("Grimoire_Front");
                    weapon.transform.localPosition = Vector3.zero;
                }
                averyFront.SetActive(true);
                averyRight.SetActive(false);
                averyBack.SetActive(false);
                averyLeft.SetActive(false);
                anim = averyFront.GetComponent<Animator>();
                ChangeAnimationState("WalkFront_player");
            }
            else if (verticalInput > 0)
            {
                if (weapon.transform.childCount > 0)
                {
                    anim = weapon.GetComponentInChildren<Animator>();
                    ChangeAnimationState("Grimoire_Back");
                    weapon.transform.localPosition = new Vector3(-2f, 0, 0);
                }
                averyBack.SetActive(true);
                averyFront.SetActive(false);
                averyRight.SetActive(false);
                averyLeft.SetActive(false);
                anim = averyBack.GetComponent<Animator>();
                ChangeAnimationState("WalkBack_player");
            }
            else if (horizontalInput > 0)
            {
                if (weapon.transform.childCount > 0)
                {
                    anim = weapon.GetComponentInChildren<Animator>();
                    ChangeAnimationState("Grimoire_Side");
                    weapon.transform.localPosition = Vector3.zero;
                }
                averyRight.SetActive(true);
                averyBack.SetActive(false);
                averyFront.SetActive(false);
                averyLeft.SetActive(false);
                anim = averyRight.GetComponent<Animator>();
                ChangeAnimationState("WalkRight_player");
            }
            else if (horizontalInput < 0)
            {
                if (weapon.transform.childCount > 0)
                {
                    anim = weapon.GetComponentInChildren<Animator>();
                    ChangeAnimationState("Grimoire_Side");
                    weapon.transform.localPosition = new Vector3(-2f, 0, 0);
                }
                averyLeft.SetActive(true);
                averyRight.SetActive(false);
                averyFront.SetActive(false);
                averyBack.SetActive(false);
                anim = averyLeft.GetComponent<Animator>();
                ChangeAnimationState("WalkLeft_player");
            }
        }
        else
        {
            if (playerAttack == false)
            {
                if (anim == averyFront.GetComponent<Animator>())
                    ChangeAnimationState("IdleFront_player");
                else if (anim == averyBack.GetComponent<Animator>())
                    ChangeAnimationState("IdleBack_player");
                else if (anim == averyRight.GetComponent<Animator>())
                    ChangeAnimationState("IdleRight_player");
                else if (anim == averyLeft.GetComponent<Animator>())
                    ChangeAnimationState("IdleLeft_player");
            }
        }
    }

    public void PlayerAttackAnimate(int type)
    {
        playerAttack = true;
        if (anim == averyFront.GetComponent<Animator>())
        {
            if (type == 1)
            {
                anim.SetTrigger("Front");
                StartCoroutine(playerAttDelay());
                // ChangeAnimationState("AttackFront_player");
            }


            else if (type == 2)
            {
                anim.SetTrigger("Front2");
                StartCoroutine(playerAttDelay());
                // ChangeAnimationState("AttackFront2_player");
            }

        }
        else if (anim == averyBack.GetComponent<Animator>())
        {
            if (type == 1)
            {
                anim.SetTrigger("Back");
                StartCoroutine(playerAttDelay());
                // ChangeAnimationState("AttackBack_player");
            }
            else if (type == 2)
            {
                anim.SetTrigger("Back2");
                StartCoroutine(playerAttDelay());
                // ChangeAnimationState("AttackBack2_player");
            }

        }
        else if (anim == averyRight.GetComponent<Animator>())
        {
            if (type == 1)
            {
                anim.SetTrigger("Right");
                StartCoroutine(playerAttDelay());
                // ChangeAnimationState("AttackRight_player");
            }

            else if (type == 2)
            {
                anim.SetTrigger("Right2");
                StartCoroutine(playerAttDelay());
                // ChangeAnimationState("AttackRight2_player");
            }

        }
        else if (anim == averyLeft.GetComponent<Animator>())
        {
            if (type == 1)
            {
                anim.SetTrigger("Left");
                StartCoroutine(playerAttDelay());
                // ChangeAnimationState("AttackLeft_player");
            }

            else if (type == 2)
            {
                anim.SetTrigger("Left2");
                StartCoroutine(playerAttDelay());
                // ChangeAnimationState("AttackLeft2_player");
            }

        }
    }

    public void CompanionMoveAnimate()
    {
        var speed = companion.GetComponent<NavMeshAgent>().velocity.magnitude;
        var movementDirection = companion.GetComponent<NavMeshAgent>().velocity.normalized;
        anim2 = companion.transform.GetChild(0).GetComponent<Animator>();
        SpriteRenderer spriteRenderer = companion.transform.GetChild(0).GetComponent<SpriteRenderer>();
        if (speed < 0.2f)
        {
            ChangeAnimationState2("Idle_Companion");
        }
        else
        {
            ChangeAnimationState2("Walk_Companion");
            if (movementDirection.x > 0)
                spriteRenderer.flipX = false;
            else if (movementDirection.x < 0)
                spriteRenderer.flipX = true;
        }
    }

    IEnumerator playerAttDelay()
    {
        yield return new WaitForSeconds(1f);
        playerAttack = false;
    }

    void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;

        anim.Play(newState);

        currentState = newState;
    }

    void ChangeAnimationState2(string newState)
    {
        if (currentState == newState) return;

        anim2.Play(newState);

        currentState = newState;
    }

}
