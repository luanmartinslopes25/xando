using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]
    private GameObject _bulletPrefab;
    [SerializeField]
    private float _bulletSpeed = 2.0f;
    [SerializeField]
    private float _bulletCooldown = 0.8f;

    private float _lastFireTime;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.B))
        {
            float timeSinceLastFire = Time.time - _lastFireTime;

            if (timeSinceLastFire >+ _bulletCooldown)
            {
                Fire();

                _lastFireTime = Time.time;
            }
        }
    }

    private void Fire()
    {
        GameObject bullet = Instantiate(_bulletPrefab, transform.position, transform.rotation);
        Rigidbody2D rigidbody2D = bullet.GetComponent<Rigidbody2D>();
        rigidbody2D.velocity = _bulletSpeed * transform.up;
    }
}
