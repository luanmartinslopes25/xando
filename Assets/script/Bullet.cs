using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 2;
    public float penetration = 0.2f;
    public float maxTime = 8;

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        StartCoroutine(TimeDespawn());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Penetration());
        }

        if (collision.gameObject.CompareTag("Bullet"))
        {
            StartCoroutine(Penetration2());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Map"))
        {
            Destroy(gameObject);
        }
    }

    IEnumerator Penetration()
    {
        yield return new WaitForSeconds(penetration);
        Destroy(gameObject);
    }

    IEnumerator Penetration2()
    {
        yield return new WaitForSeconds(2.4f);
        Destroy(gameObject);
    }

    IEnumerator TimeDespawn()
    {
        yield return new WaitForSeconds(maxTime);
        Destroy(gameObject);
    }
}
