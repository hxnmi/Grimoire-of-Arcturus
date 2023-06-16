using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveReaper : Enemy
{
    Animator anim;
    string currentState;
    float timeLeft = 3f;
    bool running = true;
    bool attacking;
    public override void Die()
    {
        //base.Die();
        Debug.Log("Cave Reaper Mati");
        checkDie = true;
        ChangeAnimationState("Die_CaveReaper");
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
        CReaperMoveAnimate(direction);
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
    }

    private void OnCollisionEnter(Collision other)
    {
        GameObject player = GetComponent<EnemyController>().player.gameObject;
        if (other.gameObject.CompareTag("Player"))
        {
            attacking = true;
            ChangeAnimationState("Attack_CaveReaper");
            GetComponent<Enemy>().Attack(Random.Range(1, 10));
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !anim.IsInTransition(0))
            {
                attacking = false;
            }
        }
    }

    public void CReaperMoveAnimate(Vector3 direction)
    {
        direction.Normalize();
        var speed = GetComponent<UnityEngine.AI.NavMeshAgent>().velocity.magnitude;
        var movementDirection = GetComponent<UnityEngine.AI.NavMeshAgent>().velocity.normalized;
        anim = transform.GetChild(0).GetComponent<Animator>();
        SpriteRenderer spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        if (speed < 0.2f)
        {
            ChangeAnimationState("Idle_CaveReaper");
        }
        else if (!attacking)
        {
            ChangeAnimationState("Walk_CaveReaper");
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

    IEnumerator DieDelay()
    {
        yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);
    }

}
