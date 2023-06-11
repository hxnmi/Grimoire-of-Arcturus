using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionController : MonoBehaviour
{
    // FSM
    enum State { Patrol, Investigate, Chase };
    State curState = State.Patrol;

    // Player info
    public Transform player;

    // Field of View settings
    public float fovDist = 40.0f;
    public float fovAngle = 45.0f;

    // Last place the player was seen 
    Vector3 lastPlaceSeen;

    // Chasing settings
    public float chasingSpeed = 4.0f;
    public float chasingRotSpeed = 4.0f;
    public float chasingAccuracy = 5.0f;

    // Patrol settings
    public float patrolDistance = 50.0f;
    float patrolWait = 5.0f;
    float patrolTimePassed = 0;

    void Chase(Transform player)
    {
        this.GetComponent<UnityEngine.AI.NavMeshAgent>().ResetPath();

        Vector3 direction = player.position - this.transform.position;

        Rigidbody rbplayer = player.GetComponent<Rigidbody>();

        if (rbplayer.velocity.magnitude < 2)
        {
            Vector3 AwayPoint = lastPlaceSeen;
            AwayPoint += new Vector3(Random.Range(-2, 2), 0, Random.Range(-2, 2));
            this.GetComponent<UnityEngine.AI.NavMeshAgent>().SetDestination(AwayPoint);
        }
        else
        {
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * this.chasingRotSpeed);
        }

        if (direction.magnitude > this.chasingAccuracy)
        {
            this.transform.Translate(0, 0, Time.deltaTime * this.chasingSpeed);
        }
    }

    void Investigate()
    {
        if (transform.position.x == lastPlaceSeen.x && transform.position.z == lastPlaceSeen.z)
        {
            curState = State.Patrol;
        }
        else
        {
            this.GetComponent<UnityEngine.AI.NavMeshAgent>().SetDestination(lastPlaceSeen);
        }
    }

    void Patrol()
    {
        patrolTimePassed += Time.deltaTime;

        if (patrolTimePassed > patrolWait)
        {
            patrolTimePassed = 0;
            Vector3 patrollingPoint = lastPlaceSeen;

            patrollingPoint += new Vector3(Random.Range(-patrolDistance, patrolDistance), 0, Random.Range(-patrolDistance, patrolDistance));

            this.GetComponent<UnityEngine.AI.NavMeshAgent>().SetDestination(patrollingPoint);
        }
    }

    public void GoTo()
    {
        this.GetComponent<UnityEngine.AI.NavMeshAgent>().SetDestination(player.position);
        lastPlaceSeen = player.position;
    }

    //IEnumerator PlayKnock()
    //{
    //    AudioSource audio = GetComponent<AudioSource>();

    //    audio.Play();
    //    yield return new WaitForSeconds(audio.clip.length); 
    //}

    bool ICanSee(Transform player)
    {
        Vector3 direction = player.position - this.transform.position;
        float angle = Vector3.Angle(direction, this.transform.forward);

        Debug.DrawRay(this.transform.position, direction, Color.green);

        RaycastHit hit;
        if (Physics.Raycast(this.transform.position, direction, out hit) && direction.magnitude < fovDist && angle < fovAngle)
        {
            return true;
        }
        return false;
    }


    void Start()
    {
        patrolTimePassed = patrolWait;
        lastPlaceSeen = this.transform.position;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void Update()
    {
        State tmpstate = curState;

        if (ICanSee(player))
        {
            curState = State.Chase;
            lastPlaceSeen = player.position;
        }
        else
        {
            if (curState == State.Chase)
            {
                curState = State.Investigate;
            }
        }

        switch (curState)
        {
            case State.Patrol:
                Patrol();
                break;
            case State.Investigate:
                Investigate();
                break;
            case State.Chase: // Move towards the player 
                Chase(player);
                break;
        }

    }
}