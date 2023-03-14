using UnityEngine;
using UnityEngine.Events;

public class Attack : MonoBehaviour
{
    public float attackRange;
    public UnityEvent attack;
    public UnityEvent lostTarget;
    public UnityEvent outOfRange;
    Vision vision;

    void Awake()
    {
        vision = GetComponent<Vision>();
    }

    void Update()
    {
        if (vision.target == null)
        {
            lostTarget.Invoke();
        }
        else if (vision.rangeToTarget > attackRange)
        {
            outOfRange.Invoke();
        }
        else
        {
            attack.Invoke();
        }
    }
}
