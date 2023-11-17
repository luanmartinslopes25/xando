using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float penetration = 0.02f;
    public float maxTime = 8;
    public int particles = 1;

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
            Splash(1.6f);
            transform.localScale = ballform;
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Wall"))
        {
            Splash(2.4f);
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

    IEnumerator TimeDespawn()
    {
        yield return new WaitForSeconds(maxTime);
        Destroy(gameObject);
    }

    IEnumerator Penetration()
    {
        yield return new WaitForSeconds(0.064f);
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
}
