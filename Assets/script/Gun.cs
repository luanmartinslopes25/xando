using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;


public class Gun : MonoBehaviour
{
    public Main mainScript;

    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private float bulletSpeed = 2.0f;
    [SerializeField]
    private float bulletCooldown = 0.8f;
    [SerializeField]
    private int bulletAmount = 1;
    private float lastFireTime;
    public float spread;


    public string fire;


    public GameObject shootAudio;


    private void Start()
    {
        shootAudio.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (mainScript.ready)
        {
            if (Input.GetButton(fire))
            {
                float timeSinceLastFire = Time.time - lastFireTime;

                if (timeSinceLastFire > +bulletCooldown)
                {
                    Fire();

                    lastFireTime = Time.time;
                }
            }
        }
    }

    private void Fire()
    {
        shootAudio.SetActive(false);
        for (int i = 0; i < bulletAmount; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            Vector2 dir = transform.rotation * Vector2.up;
            Vector2 pdir = Vector2.Perpendicular(dir) * UnityEngine.Random.Range(-spread, spread);
            rb.velocity = (dir + pdir) * bulletSpeed;
        }
        shootAudio.SetActive(true);
    }
}
