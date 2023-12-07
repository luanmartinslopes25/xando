using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Players : MonoBehaviour
{
    private Rigidbody2D rb;
    public float moveSpeed = 64;
    public float spinSpeed = 48;
    public float spinResistence = 32;
    private Vector2 jumpSpeed;

    public string Horizontal;
    public string Vertical;
    public string Spin;

    public bool haveGround = true;
    public bool isJumping = false;
    public Vector3 playerSize = new Vector3(1, 1, 1);
    public Vector3 jumpHeight = new Vector3(1.5f, 1.5f, 1.5f);
    public float jumpSpeedMulti = 16;
    public float gravity = 0.05f;

    public float acceleration = 48;
    public float maxSpeed = 56;

    //public mainScript mainScript;

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

      // Move
        float moveHorizontal = Input.GetAxis(Horizontal);
        float moveVertical = Input.GetAxis(Vertical);

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        movement = movement.normalized;

        float currentSpeed = Vector2.Dot(rb.velocity, movement);
        float speedDiff = maxSpeed - currentSpeed;
        float requiredAcceleration = Mathf.Clamp(speedDiff / Time.deltaTime, 0, acceleration);

        if (!isJumping)
        {
            rb.AddForce(movement * requiredAcceleration);
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
            Die();
        }
    }

    private void JumpDown()
    {
        transform.localScale -= new Vector3(gravity, gravity, gravity);
    }

    private void Die()
    {
        
    }
}
