using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCPathing : MonoBehaviour
{
    [SerializeField]
    Transform dest;

    [SerializeField]
    bool patrolWaiting;

    [SerializeField]
    float totalWaitTime = 3f;

    [SerializeField]
    float switchProbability = 0.2f;

    [SerializeField]
    List<Waypoint> patrolPoints;

    NavMeshAgent navMeshAgent;
    int currentPatrolIndex;
    bool travelling;
    bool waiting;
    bool patrolForward;
    float waitTimer;

    // Use this for initialization
    void Start()
    {
        navMeshAgent = this.GetComponent<NavMeshAgent>();

        if (navMeshAgent != null)
        {
            if(patrolPoints != null && patrolPoints.Count >= 2)
            {
                currentPatrolIndex = 0;
                SetDestination();
            }
            else
            {
                Debug.Log("Not enough patrol points");
            }

                
        }
        
    }

    private void Update()
    {
        if(travelling && navMeshAgent.remainingDistance <= 1.0f)
        {
            travelling = false;

            if(patrolWaiting)
            {
                waiting = true;
                waitTimer = 0f;
            }
            else
            {
                ChangePatrolPoint();
                SetDestination();
            }
            
        }

        if(waiting)
        {
            waitTimer += Time.deltaTime;
            if(waitTimer >= totalWaitTime)
            {
                waiting = false;

                ChangePatrolPoint();
                SetDestination();
            
            }

        }

    }

    private void SetDestination()
    {

        if(patrolPoints != null)
        {
            Vector3 targetVector = patrolPoints[currentPatrolIndex].transform.position;
            navMeshAgent.SetDestination(targetVector);
            travelling = true;
        }
    }

    public void ChangePatrolPoint()
    {
        if(UnityEngine.Random.Range(0f, 1f) <= switchProbability)
        {
            patrolForward = !patrolForward; //boolean value flips to other state
        }

        if(patrolForward)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Count; //check if index exceeds total number of points
        }
        else
        {
            if(--currentPatrolIndex < 0)
            {
                currentPatrolIndex = patrolPoints.Count - 1;
            }
        }


    }
}
