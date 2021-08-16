using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Gun : MonoBehaviour
{
    private PhotonView pv;
    public int projectiles = 1;
    public float shotRange = 1.0f;
    //firerate per gun
    public float fireRate = 0.5f;
    private float firerate = 0.1f;
    public Transform firepoint;

    public GameObject bulletPrefab;
    public GameObject[] guns;

    private void Start()
    {
        pv = GetComponentInParent<PhotonView>();
    }

    void Update()

    {
        if (!pv.IsMine && PhotonNetwork.IsConnected) return;
        firerate -= Time.deltaTime;
        if (Input.GetMouseButton(0) && firerate <= 0)
        {
            firerate = fireRate;
            Shoot();
        }
    }

    public void Shoot()
    {
        if(projectiles == 1)
        {
            //shooting logic
            var spawnedBullet =  PhotonNetwork.Instantiate(bulletPrefab.name, firepoint.position, firepoint.rotation);
            //Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);
            spawnedBullet.GetComponent<Rigidbody2D>().AddForce(firepoint.right * shotRange); 
        }
        //else
        //{
        //    Debug.Log("Multiple Projectiles");
        //    for (int i = 0; i < projectiles; i++)
        //    {
        //        Debug.Log(i);
        //        //shooting logic
        //        var spawnedBullet = Instantiate(bulletPrefab, firepoint.position * Random.Range(-.75f,.75f), firepoint.rotation);
        //        //Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);
        //        spawnedBullet.GetComponent<Rigidbody2D>().AddForce(firepoint.right * shotRange);
        //    }
        //}
    }
}
