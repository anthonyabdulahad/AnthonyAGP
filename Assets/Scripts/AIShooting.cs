using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIShooting : MonoBehaviour
{
    public GameObject enemyBullet;
    public Transform shootPoint;
    public float enemyBulletSpeed = 10.0f;
    public float reloadTime = 1.0f;
    float reload;
    public void Shoot()
    {
        if (reload > reloadTime)
        {
            GameObject tempBullet = Instantiate(enemyBullet, shootPoint.position, shootPoint.rotation);
            Rigidbody tempRigidBodyBullet = tempBullet.GetComponent<Rigidbody>();
            tempRigidBodyBullet.AddForce(tempRigidBodyBullet.transform.forward * enemyBulletSpeed, ForceMode.Impulse);
            Destroy(tempBullet.gameObject, 100f);
            reload = 0.0f;
        }
        else
        {
            reload += Time.deltaTime;
        }
    }
}
