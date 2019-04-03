using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCPathing : MonoBehaviour
{
    [SerializeField]
    Transform dest;

    NavMeshAgent navMeshAgent;

    // Use this for initialization
    void Start()
    {
        navMeshAgent = this.GetComponent<NavMeshAgent>();

        if (navMeshAgent != null)
        {
            SetDestination();
        }
        
    }

    private void SetDestination()
    {
        if (dest != null)
        {
            Vector3 targetVector = dest.transform.position;
            navMeshAgent.SetDestination(targetVector);
        }
    }
}