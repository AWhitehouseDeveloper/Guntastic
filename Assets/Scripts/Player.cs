using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    private float x;
    private Rigidbody2D rb;
    private bool onFloor = false;
    private int jumps = 0;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxis("Horizontal");

        transform.position += (Vector3) new Vector2(x * speed * Time.deltaTime, 0);
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
}
