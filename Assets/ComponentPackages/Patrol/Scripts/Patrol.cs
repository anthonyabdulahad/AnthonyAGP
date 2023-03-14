using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(NavMeshAgent))]
public class Patrol : MonoBehaviour
{
    public UnityEvent onWaypointReached;
    public WayPoint[] wayPoints;
    NavMeshAgent agent;
    int currentWaypoint;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        currentWaypoint = 0;
    }

    void OnEnable()
    {
        WayPoint wayPoint = wayPoints[currentWaypoint];
        agent.SetDestination(wayPoint.transform.position);
        agent.isStopped = false;
    }

    void Update()
    {
        if (!agent.hasPath)
        {
            enabled = false;
            onWaypointReached.Invoke();
            currentWaypoint++;
            if (currentWaypoint >= wayPoints.Length)
            {
                currentWaypoint = 0;
            }
        }
    }
}
