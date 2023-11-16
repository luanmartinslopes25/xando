using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float penetration = 0.2f;
    public float maxTime = 8;

    Rigidbody2D rb;

    private Vector3 ballform = new Vector3(0.08f, 0.04f, 1);

    [SerializeField]
    private GameObject waterdrop;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        StartCoroutine(TimeDespawn());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Penetration());
            transform.localScale = ballform;
        }

        if (collision.gameObject.CompareTag("Bullet"))
        {
            StartCoroutine(Penetration2());
            transform.localScale = ballform;
        }

        if (collision.gameObject.CompareTag("Wall"))
        {
            Splash();
            Destroy(gameObject);
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
        Splash();
        Destroy(gameObject);
    }

    IEnumerator Penetration2()
    {
        yield return new WaitForSeconds(penetration * 2);
        Destroy(gameObject);
    }

    IEnumerator TimeDespawn()
    {
        yield return new WaitForSeconds(maxTime);
        Destroy(gameObject);
    }

    private void Splash()
    {
        GameObject drop1 = Instantiate(waterdrop, transform.position, transform.rotation);
        GameObject drop2 = Instantiate(waterdrop, transform.position, transform.rotation);
        Rigidbody2D rb1 = drop1.GetComponent<Rigidbody2D>();
        Rigidbody2D rb2 = drop2.GetComponent<Rigidbody2D>();
        rb1.velocity = 8 * transform.up;
        rb2.velocity = 8 * transform.up;
    }
}
