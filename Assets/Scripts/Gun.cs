using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public int projectiles = 1;
    public float shotRange = 1.0f;
    public float fireRate = 1.0f;
    private float firerate = 1.0f;
    public Transform firepoint;

    public GameObject bulletPrefab;


    void Update()
    {
        firerate -= Time.deltaTime;
        if (Input.GetMouseButtonDown(0) && firerate <= 0)
        {
            firerate = fireRate;
            Shoot();
        }
    }

    public void Shoot()
    {
        //shooting logic
        //var spawnedBullet = Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);
        Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);
        //spawnedBullet.AddForce(firepoint.right * shotRange);
    }
}
