using UnityEngine;
using UnityEngine.Events;

public class Vision : MonoBehaviour
{
    public float viewDistance = 10.0f;
    public float fov = 90.0f;
    public UnityEvent onSpotted;
    public LayerMask layerMask;
    CharacterController player;
    

    internal Transform target;
    internal Vector3 targetVelocity;
    internal float rangeToTarget;
    internal Vector3 lastKnownPosition;

    void Awake()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.GetComponent<CharacterController>();
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawFrustum(Vector3.zero, fov, viewDistance, 0.5f, 1.0f);
        Gizmos.DrawWireSphere(Vector3.zero, viewDistance);
    }

    void Update()
    {
        if (player != null)
        {
            Vector3 toPlayer = player.transform.position - transform.position;
            float distance = toPlayer.magnitude;
            RaycastHit hit;
            bool canSee = !Physics.Raycast(new Ray(transform.position, transform.forward), out hit, viewDistance, layerMask);
            if (!canSee)
            {
                Debug.Log($"View blocked by {hit.collider.name}", hit.collider.gameObject);
            }
            if (distance < viewDistance && Vector3.Angle(toPlayer, transform.forward) < 0.5f * fov && canSee)
            {
                rangeToTarget = distance;
                if (target == null)
                {
                    onSpotted.Invoke();
                }
                target = player.transform;
                targetVelocity = player.velocity;
            }
            else
            {
                if (target != null)
                {
                    lastKnownPosition = target.transform.position;
                }
                target = null;
                targetVelocity = Vector3.zero;
            }
        }
    }

   
}