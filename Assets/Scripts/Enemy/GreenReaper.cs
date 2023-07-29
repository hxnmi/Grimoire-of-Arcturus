using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenReaper : Enemy
{
    Animator anim;
    string currentState;
    float timeLeft = 3f;
    bool running = true;
    bool attacking = false;
    public override void Die()
    {
        //base.Die();
        Debug.Log("Green Reaper Mati");
        checkDie = true;
        ChangeAnimationState("Die_GreenReaper");
        StartCoroutine(DieDelay());
    }

    private void Start()
    {

        hp = 100;
        maxHp = hp;

    }
    private void FixedUpdate()
    {
        Vector3 direction = GetComponent<EnemyController>().direction;
        GReaperMoveAnimate(direction);
        if (running)
        {
            if (!EnemySensor.CurrentTargetObject && hp < maxHp)
            {
                timeLeft -= Time.deltaTime;
                if (timeLeft <= 0)
                {
                    Heal(Random.Range(1, 10));
                    running = false;
                }
            }
        }
        else
        {
            timeLeft = 3f;
            running = true;
        }

        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !anim.IsInTransition(0))
            attacking = false;

    }

    private void OnTriggerEnter(Collider other)
    {
        attacking = true;
        GameObject player = GetComponent<EnemyController>().player.gameObject;
        if (other.gameObject.CompareTag("Player"))
        {
            if (attacking)
            {
                Attack(Random.Range(1, 10));
                ChangeAnimationState("Attack_GreenReaper");
                GameObject.FindGameObjectWithTag("Player").transform.GetChild(6).GetChild(2).gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        attacking = false;
        GameObject.FindGameObjectWithTag("Player").transform.GetChild(6).GetChild(2).gameObject.SetActive(false);
    }

    IEnumerator DieDelay()
    {
        yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);
    }

    public void GReaperMoveAnimate(Vector3 direction)
    {
        direction.Normalize();
        var speed = GetComponent<UnityEngine.AI.NavMeshAgent>().velocity.magnitude;
        var movementDirection = GetComponent<UnityEngine.AI.NavMeshAgent>().velocity.normalized;
        anim = transform.GetChild(0).GetComponent<Animator>();
        SpriteRenderer spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        if (!attacking)
        {
            if (speed < 0.2f)
            {
                ChangeAnimationState("Idle_GreenReaper");
            }
            else
            {
                ChangeAnimationState("Walk_GreenReaper");
                if (movementDirection.x > 0 || direction.x > 0)
                    spriteRenderer.flipX = false;
                else if (movementDirection.x < 0 || direction.x < 0)
                    spriteRenderer.flipX = true;
            }
        }
    }

    void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;

        anim.Play(newState);

        currentState = newState;
    }
}
