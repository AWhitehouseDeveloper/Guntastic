using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public float size;
    public float speed = 60f;

    public Rigidbody2D rb;
    public GameObject impactEffect;

    public float bulletTimelimit = 8f;

    void Start()
    {
        bulletTimelimit = 3f;
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
            player.TakeDamage(damage);
        }
        //Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
