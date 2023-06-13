using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAt : MonoBehaviour
{
    [SerializeField] Sprite reticle1;
    [SerializeField] Sprite reticle2;
    [SerializeField] GameObject attackRotate;
    [SerializeField] GameObject player;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        PlayerMovement playerMov = player.GetComponent<PlayerMovement>();
        // EnemyAt Rotation
        if (playerMov.HorizontalInput != 0 || playerMov.VerticalInput != 0)
        {
            if (!EnemySensor.CurrentTargetObject)
            {
                transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = reticle1;
                Quaternion toRotation = Quaternion.LookRotation(playerMov.MoveDirection);
                attackRotate.transform.rotation = Quaternion.RotateTowards(attackRotate.transform.rotation, toRotation, 1000 * Time.deltaTime);
            }
            else
                transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = reticle2;
        }
    }
}
