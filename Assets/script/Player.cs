using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;

    private float moveVertical;
    private float moveHorizontal;
    public float moveSpeed = 2.0f;
    public float spinSpeed = 2.0f;

    public string up;
    public string down;
    public string left;
    public string right;
    public string spinL;
    public string spinR;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Spin
        if (Input.GetKey(KeyCode.M))
        {
            rb.rotation += spinSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.M))
        {
            rb.rotation -= spinSpeed * Time.deltaTime;
        }

        // Moviment
        if (Input.GetKey(KeyCode.W))
        {
            if (moveVertical <= 1)
            {
                moveVertical += 0.16f;
            }
        }
        else if (Input.GetKey(KeyCode.S))
        {
            if (moveVertical >= -1)
            {
                moveVertical -= 0.16f;
            }
        }
        else
        {
            moveVertical = 0;
        }

        if (Input.GetKey(KeyCode.A))
        {
            if (moveHorizontal >= -1)
            {
                moveHorizontal -= 0.16f;
            }
        }
        else if (Input.GetKey(KeyCode.D))
        {
            if (moveHorizontal <= 1)
            {
                moveHorizontal += 0.16f;
            }
        }
        else
        {
            moveHorizontal = 0;
        }
        // Calcula o vetor de movimento
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        // Normaliza o vetor de movimento para que o jogador nao se mova mais rapido na diagonal
        movement = movement.normalized;
        // Move o jogador usando o Rigidbody2D
        rb.velocity = movement * moveSpeed;

        // Fire
    }
}
