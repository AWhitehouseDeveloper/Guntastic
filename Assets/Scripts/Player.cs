using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Gun currentGun;
    public HealthBar healthBar;
    public Gun[] guns = new Gun[4];
    public Transform weaponPlacement;
    private Rigidbody2D rb;
    public Transform weaponPoint;
    private float x;
    public float speed, jumpForce;
    private SpriteRenderer sprite;

    private bool onFloor = false;
    private int jumps = 0, kills = 0;
    private float health = 0;
    private float maxHealth = 100f;
    void Start()
    {
        currentGun = guns[0];
        currentGun = Instantiate(currentGun, new Vector3( weaponPlacement.transform.position.x, weaponPlacement.transform.position.y, 0), Quaternion.identity);
        currentGun.transform.parent = gameObject.transform;
        //Instantiate(guns[1], weaponPlacement.transform.position, weaponPlacement.transform.rotation);
        rb = GetComponent<Rigidbody2D>();
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        //you get coloured but other players do not.
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxis("Horizontal");
        if(x < 0)
        {
            sprite.flipX = true;
            weaponPlacement.position = new Vector2(-weaponPlacement.position.x, weaponPlacement.position.y);
        }
        if(x > 0)
        {
            weaponPlacement.position = new Vector2(Math.Abs(weaponPlacement.position.x), weaponPlacement.position.y);
            sprite.flipX = false;
        }
        transform.position += (Vector3) new Vector2(x * speed * Time.deltaTime, 0);
        //TakeDamage(5);
    }

    private void FixedUpdate()
    {
            Debug.Log("fixed");
        if (Input.GetButtonDown("Jump") && (onFloor || jumps < 2))
        {
            Debug.Log(jumps);
            jumps++;
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            Debug.Log("floor enter");
            onFloor = true;
            jumps = 0;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Floor")
        {
            Debug.Log("floor exit");
            onFloor = false;
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        healthBar.SetHealth(health);
    }

    public void OnKill()
    {
        kills++;
        if (kills < 4)
        {
            currentGun = guns[kills];
        }
        else
        {
            Debug.Log("Someone won!");
            return;
        }
    }
}
