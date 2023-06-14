using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueReaper : Enemy
{
    float timeLeft = 3f;
    bool running = true;
    public override void Die()
    {
        //base.Die();
        Debug.Log("Goblin Mati");
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
        GameObject.FindWithTag("GameController").GetComponent<Animation>().BReaperMoveAnimate(direction);
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
}
