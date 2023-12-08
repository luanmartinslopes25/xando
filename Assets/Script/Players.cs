using Newtonsoft.Json.Bson;
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
    private Vector2 movement;

    private Vector2 jumpSpeed;
    public bool haveGround = true;
    public bool isJumping = false;
    public Vector3 playerSize = new Vector3(1, 1, 1);
    public Vector3 jumpHeight = new Vector3(1.5f, 1.5f, 1.5f);
    public float jumpSpeedMulti = 16;
    public float gravity = 0.05f;

    public int score;

    public int gun;
    public GameObject gun1;
    public GameObject gun2;
    public GameObject gun3;

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

            movement = new Vector2(moveHorizontal, moveVertical);
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
            if (mainScript.ready)
            {
                if (!isJumping)
                {
                    StartCoroutine(JumpUpCoroutine());
                }

            }
        }
           
    }

    private IEnumerator JumpUpCoroutine()
    {
        isJumping = true;
        Vector2 jumpBalanced = movement;
        rb.velocity = rb.velocity + jumpBalanced*1.5f;
        yield return new WaitForSeconds(0.01f);
        rb.velocity = rb.velocity + jumpBalanced;
        yield return new WaitForSeconds(0.01f);
        rb.velocity = rb.velocity + jumpBalanced/1.5f;
        yield return new WaitForSeconds(0.01f);
        rb.velocity = rb.velocity + jumpBalanced/3;
        yield return new WaitForSeconds(0.01f);

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
        if (mainScript.ready)
        {
            mainScript.ready = false;
            transform.localScale = Vector3.zero;
            score++;
            StartCoroutine(mainScript.CountdownStart());
        }
        else
        {
            transform.localScale = playerSize;
        }
    }

    public void Respawn()
    {
        transform.rotation = new Quaternion(0, 0, UnityEngine.Random.Range(-180, 180), 0);
        transform.localScale = playerSize;

        gun = UnityEngine.Random.Range(1, 4);
        if     (gun == 1)
        {
            gun1.gameObject.SetActive(true);
            gun2.gameObject.SetActive(false);
            gun3.gameObject.SetActive(false);
        }
        else if(gun == 2)
        {
            gun1.gameObject.SetActive(false);
            gun2.gameObject.SetActive(true);
            gun3.gameObject.SetActive(false);
        }
        else if(gun == 3)
        {
            gun1.gameObject.SetActive(false);
            gun2.gameObject.SetActive(false);
            gun3.gameObject.SetActive(true);
        }

    }
}