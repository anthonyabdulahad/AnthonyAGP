using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Chase : MonoBehaviour
{
    public float speed = 2.5f;
    public float attackRange = 2.0f;
    public UnityEvent inRange;
    public UnityEvent targetLost;
    NavMeshAgent agent;
    Vision vision;
    

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        vision = GetComponent<Vision>();
    }

    void Update()
    {
        if (vision.target!= null)
        {
            Vector3 targetDirection = vision.target.position - transform.position;
            float singleStep = speed * Time.deltaTime;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDirection);
        }
        

        if (vision.target != null)
        {
            if (vision.rangeToTarget < attackRange)
            {
                inRange.Invoke();
                agent.isStopped = true;
            }
            else
            {
                agent.isStopped = false;
                agent.SetDestination(vision.target.position);
            }
        }
        else
        {
            targetLost.Invoke();
        }
    }
}
