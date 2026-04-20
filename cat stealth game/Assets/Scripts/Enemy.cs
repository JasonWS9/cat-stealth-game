using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public List<Transform> movePoints;
    private int movePointIndex;

    private NavMeshAgent agent;

    void Start()
    {
        
    }

    void Update()
    {
        movePointIndex = 1;
        Transform currentMovePoint = movePoints[movePointIndex];

        //agent.destination = currentMovePoint;

        if (transform.position == currentMovePoint.position)
        {
            movePointIndex ++;
            currentMovePoint = movePoints[movePointIndex];    
        }
    }
}
