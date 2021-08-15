using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public float size;
    public float speed = 20f;

    public Rigidbody2D rb;
    public GameObject impactEffect;

    public float bulletTimelimit = 2f;

    void Start()
    {
        bulletTimelimit = 2f;
        rb.velocity = transform.right * speed;
    }

    void Update()
    {
        bulletTimelimit -= Time.deltaTime;
        if(bulletTimelimit <= 0)
        {
            Destroy(gameObject);
        }
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if(player != null)
        {
           // player.Takedamage()
        }
        //Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
