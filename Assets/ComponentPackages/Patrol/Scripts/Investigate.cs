using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Investigate : MonoBehaviour
{
    public UnityEvent onTargetNotFound;
    Vision vision;
    NavMeshAgent agent;

    void Awake()
    {
        vision = GetComponent<Vision>();
        agent = GetComponent<NavMeshAgent>();
    }

    void OnEnable()
    {
        agent.SetDestination(vision.lastKnownPosition);
    }

    void Update()
    {
        if (!agent.hasPath)
        {
            onTargetNotFound.Invoke();
        }
    }
}
