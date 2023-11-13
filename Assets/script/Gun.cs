using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private float bulletSpeed = 2.0f;
    [SerializeField]
    private float bulletCooldown = 0.8f;

    private float lastFireTime;

    private float spread;
    public float spreadAngle;
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.B))
        {
            float timeSinceLastFire = Time.time - lastFireTime;

            if (timeSinceLastFire >+ bulletCooldown)
            {
                Fire();

                lastFireTime = Time.time;
            }
        }

        spread = UnityEngine.Random.Range(-spreadAngle, spreadAngle);
        Debug.Log(spread);

        Debug.Log(transform.rotation.ToString());
    }

    private void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        Transform bulletTransform = bullet.GetComponent<Transform>();
        rb.velocity = bulletSpeed * transform.up;
    }
}
