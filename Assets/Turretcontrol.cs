using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turretcontrol : MonoBehaviour
{

    CharacterController Player;
    float dist;
    public float Maxdist;
    public float projectilespeed;
    public Transform head, barrel;
    public GameObject projectile;
    public float fireRate, nextFire;
    public float initialMissDistance = 1.0f;
    float missDistance;

    // Start is called before the first frame update
    void Start()
    {
        missDistance = initialMissDistance;
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        dist = Vector3.Distance(Player.transform.position, transform.position);
        if(dist <= Maxdist)
        {
            Vector3 prediction = PredictPlayerPosition(Player.transform.position, Player.velocity);
            head.LookAt(prediction);
            if (Time.time >= nextFire)
            {
                nextFire = Time.time +1f / fireRate;
                head.LookAt(prediction + Random.onUnitSphere * missDistance);
                shoot();
                missDistance /= 2.0f;
            }
           
        }
        else
        {
            missDistance = initialMissDistance;
        }

    }

    private Vector3 PredictPlayerPosition(Vector3 position, Vector3 velocity)
    {
        float a = Vector3.Dot(velocity, velocity) - projectilespeed * projectilespeed;
        float b = 2.0f * (Vector3.Dot(position, velocity) - Vector3.Dot(barrel.position, velocity));
        float c = Vector3.Dot(position, position) + Vector3.Dot(barrel.position, barrel.position) - 2.0f * (Vector3.Dot(barrel.position, position));

        float t1 = (-b + Mathf.Sqrt((b * b) - (4.0f * a * c))) / (2.0f * a);
        float t2 = (-b - Mathf.Sqrt((b * b) - (4.0f * a * c))) / (2.0f * a);

        float t = 1.0f;
        if (t1 < t2 && t1 > 0.0f)
        {
            t = t1;
        }
        else if (t2 > 0.0f)
        {
            t = t2;
        }

        return position + velocity * t;
    }

    void shoot()
    {
       GameObject clone = Instantiate(projectile, barrel.position, head.rotation);
        clone.GetComponent<Rigidbody>().AddForce(head.forward * projectilespeed, ForceMode.VelocityChange);
       
        //force Forward

    }
}
