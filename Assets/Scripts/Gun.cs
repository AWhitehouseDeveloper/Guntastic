using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    public int projectiles = 1;
    public float shotRange = 1.0f;
    //firerate per gun
    public float fireRate = 1.0f;
    private float firerate = 0.1f;
    public Transform firepoint;

    public GameObject bulletPrefab;
    public GameObject[] guns;

    void Update()

    {
        firerate -= Time.deltaTime;
        if (Input.GetMouseButton(0) && firerate <= 0)
        {
            firerate = fireRate;
            Shoot();
        }
    }

    public void Shoot()
    {
        //shooting logic
        var spawnedBullet = Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);
        //Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);
        spawnedBullet.GetComponent<Rigidbody>().AddForce(firepoint.right * shotRange);
    }
}
