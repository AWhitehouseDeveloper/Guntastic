using Photon.Pun;
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
    private float x;
    public float speed, jumpForce;
    private SpriteRenderer sprite;
    PhotonView photonView;

    private bool onFloor = false;
    private int jumps = 0, kills = 0;
    private float health = 0;
    private float maxHealth = 100f;
    void Start()
    {
        photonView = GetComponent<PhotonView>();
        currentGun = guns[0];
        currentGun = Instantiate(currentGun, new Vector3(weaponPlacement.transform.position.x, weaponPlacement.transform.position.y, 0), Quaternion.identity);
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
        if (!photonView.IsMine && PhotonNetwork.IsConnected) return;
        x = Input.GetAxis("Horizontal");
        if(x < 0)
        {
            gameObject.transform.localRotation = new Quaternion(0, 180, 0, 0);
            weaponPlacement.localPosition = new Vector2(-(weaponPlacement.position.x), weaponPlacement.position.y);
        }
        else if(x > 0)
        {
            gameObject.transform.localRotation = new Quaternion(0, 0, 0, 0);
            weaponPlacement.localPosition = new Vector2(Math.Abs(weaponPlacement.position.x), weaponPlacement.position.y);
        }
        transform.position += (Vector3) new Vector2(x * speed * Time.deltaTime, 0);
    
        if (Input.GetButtonDown("Jump") && (onFloor || jumps < 2))
        {
            Debug.Log(jumps);
            jumps++;
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        }
        if (health <= 0) PhotonNetwork.LeaveRoom();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            onFloor = true;
            jumps = 0;
        }
        if(collision.gameObject.tag == "Death")
        {
            health = 0;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Floor")
        {
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
            //currentGun = guns[kills];
        }
        else
        {
            Debug.Log("Someone won!");
            return;
        }
    }
}
