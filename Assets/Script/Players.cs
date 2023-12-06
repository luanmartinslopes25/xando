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
    public Vector3 playerSize;

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
            Debug.Log("encostou em um móovel");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            haveGround = false;
            if (!isJumping)
            {
                StartCoroutine(JumpUp());
            }
            Debug.Log("saiu");
        }
           
    }

    IEnumerator JumpUp()
    {
        yield return new WaitForSeconds(0.16f);
    }

    IEnumerator JumpDown()
    {
        yield return new WaitForSeconds(0.16f);
    }
}
