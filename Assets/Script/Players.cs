using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Players : MonoBehaviour
{
    private Rigidbody2D rb;
    public float moveSpeed = 64;
    public float spinSpeed = 168.0f;
    public float spinResistence = 32.0f;
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

        // Calcula a diferença entre a velocidade desejada e a velocidade atual
        float speedDiff = maxSpeed - rb.velocity.magnitude;

        // Calcula a força necessária para atingir a velocidade desejada
        float forceMagnitude = Mathf.Min(speedDiff, acceleration);

        // Aplica a força
        if (!isJumping)
        {
            rb.AddForce(movement * forceMagnitude);
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
    }

    private void JumpDown()
    {
        transform.localScale -= new Vector3(gravity, gravity, gravity);
    }
}
