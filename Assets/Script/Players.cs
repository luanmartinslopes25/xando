using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Players : MonoBehaviour
{
    private Rigidbody2D rb;

    private float moveVertical;
    private float moveHorizontal;
    public float moveSpeed = 2.0f;
    public float spinSpeed = 168.0f;
    public float spinResistence = 32.0f;


    public KeyCode Up, Down, Left, Rigth, SpinL, SpinR;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.inertia = spinResistence;
    }

    // Update is called once per frame
    void Update()
    {
      // Spin
        if (Input.GetKey(SpinL))
        {
            rb.rotation += spinSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(SpinR))
        {
            rb.rotation -= spinSpeed * Time.deltaTime;
        }

      // Moviment
        if (Input.GetKey(Up)) 
        {
            if(moveVertical <= 1)
            {
                moveVertical += 1;
            }
        }
        else if (Input.GetKey(Down))
        {
            if (moveVertical >= -1)
            {
                moveVertical -= 1;
            }
        }
        else
        {
            moveVertical = 0;
        }

        if (Input.GetKey(Left))
        {
            if (moveHorizontal >= -1)
            {
                moveHorizontal -= 1;
            }
        }
        else if (Input.GetKey(Rigth))
        {
            if (moveHorizontal <= 1)
            {
                moveHorizontal += 1;
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
    }
}
