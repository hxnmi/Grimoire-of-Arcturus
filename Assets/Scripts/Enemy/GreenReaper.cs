using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenReaper : Enemy
{
    Animator anim;
    string currentState;
    float timeLeft = 3f;
    bool running = true;
    public override void Die()
    {
        //base.Die();
        Debug.Log("Green Reaper Mati");
        Destroy(this.gameObject);
        //animasi beda
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
    }

    private void OnCollisionEnter(Collision other)
    {
        GameObject player = GetComponent<EnemyController>().player.gameObject;
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(EnemyAttDelay());
        }
    }

    IEnumerator EnemyAttDelay()
    {
        yield return new WaitForSeconds(1f);
        Attack(Random.Range(1, 10));
        //animasi magic
        GameObject.FindGameObjectWithTag("Player").transform.GetChild(6).GetChild(2).gameObject.SetActive(true);
    }


    public void GReaperMoveAnimate(Vector3 direction)
    {
        direction.Normalize();
        var speed = GetComponent<UnityEngine.AI.NavMeshAgent>().velocity.magnitude;
        var movementDirection = GetComponent<UnityEngine.AI.NavMeshAgent>().velocity.normalized;
        anim = transform.GetChild(0).GetComponent<Animator>();
        SpriteRenderer spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
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

    void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;

        anim.Play(newState);

        currentState = newState;
    }
}
