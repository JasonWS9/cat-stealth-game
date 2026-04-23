using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public List<Transform> movePoints;
    private int movePointIndex;

    private NavMeshAgent agent;

    public float moveSpeed;
    public float pauseTime;

    private Transform currentMovePoint;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;

        movePointIndex = 0;
        currentMovePoint = movePoints[movePointIndex];
        agent.SetDestination(currentMovePoint.position);
    }

    void Update()
    {
        if (agent.remainingDistance < 0.3f)
        {
            movePointIndex = (movePointIndex + 1) % movePoints.Count;
            currentMovePoint = movePoints[movePointIndex];
            agent.SetDestination(currentMovePoint.position);
            StartCoroutine(PauseMovement());
        }
    }

    public IEnumerator PauseMovement()
    {
        float currentMoveSpeed = agent.speed;
        agent.speed = 0;
        yield return new WaitForSeconds(pauseTime);
        agent.speed = currentMoveSpeed;
    }
}
