using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Players : MonoBehaviour
{
    private Rigidbody2D rb;
    public float moveSpeed = 2.0f;
    public float spinSpeed = 168.0f;
    public float spinResistence = 32.0f;

    public string Horizontal;
    public string Vertical;
    public string Spin;

    public bool isGrounded = true;
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

        // Calcula o vetor de movimento
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        // Normaliza o vetor de movimento para que o jogador nao se mova mais rapido na diagonal
        movement = movement.normalized;
        // Move o jogador usando o Rigidbody2D
        rb.AddForce(movement * moveSpeed);

      // Spin
        if (movement != Vector2.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, movement);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, spinSpeed * Time.deltaTime);
        }


        // Jump

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {

        }
    }
}
