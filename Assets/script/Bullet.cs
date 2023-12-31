using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int penetration = 1;
    public float maxTime = 8;
    public int particles = 1;
    private Vector3 aceleration;

    Rigidbody2D rb;

    private Vector3 ballform = new Vector3(0.08f, 0.04f, 1);

    [SerializeField]
    private GameObject waterdrop;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.inertia = 0;

        StartCoroutine(TimeDespawn());
        aceleration = rb.velocity;
    }

    private void Update()
    {
        rb.AddForce(aceleration / 2);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Penetration());
            transform.localScale = ballform;
        }

        if (collision.gameObject.CompareTag("Wall"))
        {
            Splash(2.4f);
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Bullet"))
        {
            PenetrationBullets();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Map"))
        {
            Destroy(gameObject);
        }
    }

    IEnumerator TimeDespawn()
    {
        yield return new WaitForSeconds(maxTime);
        Destroy(gameObject);
    }

    IEnumerator Penetration()
    {
        yield return new WaitForSeconds(0.64f);
        Splash(2.4f);
        Destroy(gameObject);
    }

    private void Splash(float particleSpeed)
    {
        for (int i = 0; i < particles; i++)
        {
            GameObject drop1 = Instantiate(waterdrop, transform.position, transform.rotation);
            Rigidbody2D rb1 = drop1.GetComponent<Rigidbody2D>();
            Vector2 dir = transform.rotation * Vector2.up;
            Vector2 pdir = Vector2.Perpendicular(dir) * UnityEngine.Random.Range(-0.64f, 0.64f);
            rb1.velocity = (dir + pdir) * particleSpeed;
        }
    }

    private void PenetrationBullets()
    {
        penetration -= 1;
        if (penetration < 1)
        {
            Splash(5.6f);
            Destroy(gameObject);
        }
    }
}
