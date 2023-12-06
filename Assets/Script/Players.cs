using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Players : MonoBehaviour
{
    private Rigidbody2D rb;
    public float moveSpeed = 2.0f;
    public float spinSpeed = 168.0f;
    public float spinResistence = 32.0f;
    public Vector2 jumpSpeed;

    public string Horizontal;
    public string Vertical;
    public string Spin;

    public bool isGrounded = true;
    public bool haveGround = true;
    public bool isJumping = false;
    public Vector3 playerSize = new Vector3(1, 1, 1);
    public Vector3 jumpHeight = new Vector3(2, 2, 2);

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
        if (isGrounded)
        {
            rb.AddForce(movement * moveSpeed);
            if (movement.magnitude > 0)
            {
                jumpSpeed = movement * moveSpeed;
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
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, spinSpeed * Time.deltaTime);
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
            yield return new WaitForSeconds(0.08f);
        }
    }

    private void JumpUp()
    {
        transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
        if (transform.localScale.y >= jumpHeight.y)
        {
            StartCoroutine(JumpDownCoroutine());
        }
        Debug.Log("subiu");
    }

    private IEnumerator JumpDownCoroutine()
    {
        isJumping = true;

        while (transform.localScale.y > jumpHeight.y)
        {
            JumpDown();
            yield return new WaitForSeconds(0.08f);
        }
    }

    private void JumpDown()
    {
        transform.localScale += new Vector3(-0.1f, -0.1f, -0.1f);
        Debug.Log("desceu");
    }
}
