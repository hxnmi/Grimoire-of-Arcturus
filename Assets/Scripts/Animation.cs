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
    [SerializeField] GameObject player;
    [SerializeField] GameObject companion;
    [SerializeField] GameObject[] enemy;

    GameObject weapon;
    Animator anim;
    Animator anim2;
    Animator anim3;
    Animator anim4;
    Animator anim5;
    string currentState;
    bool walk;

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
        if (rb.velocity.magnitude > 1f)
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

    public void PlayerAttackAnimate(int type)
    {
        if (anim == averyFront.GetComponent<Animator>())
        {
            if (type == 1)
                anim.SetTrigger("Front");
            // ChangeAnimationState("AttackFront_player");
            else if (type == 2)
                anim.SetTrigger("Front2");
            // ChangeAnimationState("AttackFront2_player");
        }
        else if (anim == averyBack.GetComponent<Animator>())
        {
            if (type == 1)
                anim.SetTrigger("Back");
            // ChangeAnimationState("AttackBack_player");
            else if (type == 2)
                anim.SetTrigger("Back2");
            // ChangeAnimationState("AttackBack2_player");
        }
        else if (anim == averyRight.GetComponent<Animator>())
        {
            if (type == 1)
                anim.SetTrigger("Right");
            // ChangeAnimationState("AttackRight_player");
            else if (type == 2)
                anim.SetTrigger("Right2");
            // ChangeAnimationState("AttackRight2_player");
        }
        else if (anim == averyLeft.GetComponent<Animator>())
        {
            if (type == 1)
                anim.SetTrigger("Left");
            // ChangeAnimationState("AttackLeft_player");
            else if (type == 2)
                anim.SetTrigger("Left2");
            // ChangeAnimationState("AttackLeft2_player");
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

    public void BReaperMoveAnimate(Vector3 direction)
    {
        direction.Normalize();
        var speed = enemy[0].GetComponent<NavMeshAgent>().velocity.magnitude;
        var movementDirection = enemy[0].GetComponent<NavMeshAgent>().velocity.normalized;
        anim3 = enemy[0].transform.GetChild(0).GetComponent<Animator>();
        SpriteRenderer spriteRenderer = enemy[0].transform.GetChild(0).GetComponent<SpriteRenderer>();
        if (speed < 0.2f)
        {
            ChangeAnimationState3("Walk_BlueReaper");
        }
        else
        {
            ChangeAnimationState3("Walk_BlueReaper");
            if (movementDirection.x > 0 || direction.x > 0)
                spriteRenderer.flipX = false;
            else if (movementDirection.x < 0 || direction.x < 0)
                spriteRenderer.flipX = true;
        }
    }

    public void CReaperMoveAnimate(Vector3 direction)
    {
        direction.Normalize();
        var speed = enemy[1].GetComponent<NavMeshAgent>().velocity.magnitude;
        var movementDirection = enemy[1].GetComponent<NavMeshAgent>().velocity.normalized;
        anim4 = enemy[1].transform.GetChild(0).GetComponent<Animator>();
        SpriteRenderer spriteRenderer = enemy[1].transform.GetChild(0).GetComponent<SpriteRenderer>();
        if (speed < 0.2f)
        {
            ChangeAnimationState4("Walk_CaveReaper");
        }
        else
        {
            ChangeAnimationState4("Walk_CaveReaper");
            if (movementDirection.x > 0 || direction.x > 0)
                spriteRenderer.flipX = false;
            else if (movementDirection.x < 0 || direction.x < 0)
                spriteRenderer.flipX = true;
        }
    }

    public void GReaperMoveAnimate(Vector3 direction)
    {
        direction.Normalize();
        var speed = enemy[2].GetComponent<NavMeshAgent>().velocity.magnitude;
        var movementDirection = enemy[2].GetComponent<NavMeshAgent>().velocity.normalized;
        anim5 = enemy[2].transform.GetChild(0).GetComponent<Animator>();
        SpriteRenderer spriteRenderer = enemy[2].transform.GetChild(0).GetComponent<SpriteRenderer>();
        if (speed < 0.2f)
        {
            ChangeAnimationState5("Walk_GreenReaper");
        }
        else
        {
            ChangeAnimationState5("Walk_GreenReaper");
            if (movementDirection.x > 0 || direction.x > 0)
                spriteRenderer.flipX = false;
            else if (movementDirection.x < 0 || direction.x < 0)
                spriteRenderer.flipX = true;
        }
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
    void ChangeAnimationState3(string newState)
    {
        if (currentState == newState) return;

        anim3.Play(newState);

        currentState = newState;
    }
    void ChangeAnimationState4(string newState)
    {
        if (currentState == newState) return;

        anim4.Play(newState);

        currentState = newState;
    }
    void ChangeAnimationState5(string newState)
    {
        if (currentState == newState) return;

        anim5.Play(newState);

        currentState = newState;
    }
}
