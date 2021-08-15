using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed, jumpForce;
    public HealthBar healthBar;
    public Gun[] guns = new Gun[4];
    public GameObject weaponPlacement;
    private float x;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private bool onFloor = false;
    private int jumps = 0, maxHealth = 100, health, kills = 0;
    private Gun currentGun;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        //you get coloured but other players do not.
        sprite = GetComponent<SpriteRenderer>();
        sprite.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
    }

    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxis("Horizontal");
        if(x < 0)
        {
            sprite.flipX = true;
        }
        else
        {
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

    private void TakeDamage(int damage)
    {
        health -= damage;
        healthBar.SetHealth(health);
    }

    private void OnKill()
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
