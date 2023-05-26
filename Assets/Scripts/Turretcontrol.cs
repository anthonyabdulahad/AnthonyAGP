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
    public bool autoFire = true;

    public AudioSource explosion;
    public bool stationary;
    public int bulletBurstCount = 3;
    public float bulletBurstDelay = 2f;
    private int currentBulletShot = 0;
    private float currentTimeDelay;

    // Start is called before the first frame update
    void Start()
    {
        missDistance = initialMissDistance;
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();
    }

    bool CanShoot()
    {
        return (Time.time >= nextFire);
    }

    // Update is called once per frame
    void Update()
    {
        if (!stationary) // Shoot At Player
        {


            dist = Vector3.Distance(Player.transform.position, transform.position);
            if (dist <= Maxdist)
            {
                Vector3 prediction = PredictPlayerPosition(Player.transform.position + Vector3.up * 1.0f, Player.velocity);
                head.LookAt(prediction);
                if (CanShoot() && autoFire)
                {
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
        else // Shoot Straight
        {
            if (currentBulletShot < bulletBurstCount)
            {
                if (CanShoot() && autoFire)
                {
                    shoot();
                    currentBulletShot++;
                }
            }
            else
            {
                currentTimeDelay += Time.deltaTime;
                if (currentTimeDelay > bulletBurstDelay)
                {
                    currentBulletShot = 0;
                    currentTimeDelay = 0f;

                }
            }
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

    public void shoot()
    {
        if (CanShoot())
        {
            explosion.Play();
               GameObject clone = Instantiate(projectile, barrel.position, head.rotation);
                clone.GetComponent<Rigidbody>().AddForce(head.forward * projectilespeed, ForceMode.VelocityChange);
            nextFire = Time.time + 1f / fireRate;
        }

        //force Forward

    }
}
