﻿using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Players : MonoBehaviour
{
    public Main mainScript;

    private Rigidbody2D rb;

    public string Spin;
    public float spinSpeed = 48;
    public float spinResistence = 32;

    public string Horizontal;
    public string Vertical;
    public float moveSpeed = 64;
    private Vector2 jumpSpeed;

    public bool haveGround = true;
    public bool isJumping = false;
    public Vector3 playerSize = new Vector3(1, 1, 1);
    public Vector3 jumpHeight = new Vector3(1.5f, 1.5f, 1.5f);
    public float jumpSpeedMulti = 16;
    public float gravity = 0.05f;

    public int score;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.inertia = spinResistence;
        playerSize = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (mainScript.ready)
        {

            // Move
            float moveHorizontal = Input.GetAxis(Horizontal);
            float moveVertical = Input.GetAxis(Vertical);

            Vector2 movement = new Vector2(moveHorizontal, moveVertical);
            movement = movement.normalized;

            if (!isJumping)
            {
                rb.AddForce(movement * moveSpeed);
                if (rb.velocity.x != 0)
                {
                    jumpSpeed = rb.velocity * jumpSpeedMulti;
                }
            }
            else
            {
                rb.AddForce(jumpSpeed);
            }


            // Spin
            if (movement != Vector2.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, movement);
                float angleDifference = toRotation.eulerAngles.z - rb.rotation;
                angleDifference = Mathf.Repeat(angleDifference + 180f, 360f) - 180f;
                float angularVelocity = angleDifference * spinSpeed * Time.deltaTime;

                rb.AddTorque(angularVelocity * spinSpeed);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            haveGround = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            haveGround = false;
            if (!isJumping)
            {
                StartCoroutine(JumpUpCoroutine());
            }
        }
           
    }

    private IEnumerator JumpUpCoroutine()
    {
        isJumping = true;

        while (transform.localScale.y < jumpHeight.y)
        {
            JumpUp();
            yield return new WaitForSeconds(0.01f);
        }

        StartCoroutine(JumpDownCoroutine());
    }

    private void JumpUp()
    {
        transform.localScale += new Vector3(gravity, gravity, gravity);
    }

    private IEnumerator JumpDownCoroutine()
    {
        while (transform.localScale.y > playerSize.y)
        {
            JumpDown();
            yield return new WaitForSeconds(0.01f);
        }

        isJumping = false;
        if (!haveGround)
        {
            StartCoroutine(Die());
        }
    }

    private void JumpDown()
    {
        transform.localScale -= new Vector3(gravity, gravity, gravity);
    }

    private IEnumerator Die()
    {
        while (transform.localScale.y > 0)
        {
            transform.localScale -= new Vector3(gravity, gravity, gravity);
            yield return new WaitForEndOfFrame();
        }
        transform.localScale = Vector3.zero;
        score++;
        StartCoroutine(mainScript.CountdownStart());
    }

    public void Respawn()
    {
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
        transform.localScale = playerSize;
    }
}
