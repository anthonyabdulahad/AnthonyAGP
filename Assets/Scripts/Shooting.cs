using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Shooting : MonoBehaviour
{

    public float enemyBulletSpeed = 10.0f;
    public float reloadTime = 1.0f;
    float reload;
    RaycastHit hit;
    public CinemachineVirtualCamera aimCamera;
    public Patrol patrol;
    public AIShooting shooting;
    public Vision vision;
    public Chase chase;
    public Transform vfxRed;
    
    private void Update()
    {
        if (Input.GetMouseButton(1))
        {
            aimCamera.gameObject.SetActive(true);
        }
        if (Input.GetMouseButtonUp(1))
        {
            aimCamera.gameObject.SetActive(false);
        }
        if(Input.GetMouseButtonDown(0) && Input.GetMouseButton(1))
        {
            Hits();
        }
    }
    public void Hits()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Transform hitTransform = null;
        if (Physics.Raycast(ray, out hit, 1000))
        {
            hitTransform = hit.transform;
            Instantiate(vfxRed, hitTransform.position, Quaternion.identity);
            if (hit.transform.tag == "Enemy")
            {
                EnemyHealth enemyHealth = hit.transform.GetComponent<EnemyHealth>();
                enemyHealth.TakeDamage(20);
            }
        }
    }
   /* public void Shoot()
    {
        if (reload > reloadTime)
        {
            Transform tempBullet = Instantiate(enemyBullet, shootPoint.position, shootPoint.rotation);
            Rigidbody tempRigidBodyBullet = tempBullet.GetComponent<Rigidbody>();
            tempRigidBodyBullet.AddForce(tempRigidBodyBullet.transform.forward * enemyBulletSpeed, ForceMode.Impulse);
            Destroy(tempBullet.gameObject, 100f);
            reload = 0.0f;
        }
        else
        {
            reload += Time.deltaTime;
        }
    }*/
}
