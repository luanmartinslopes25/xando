using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]
    private GameObject _bulletPrefab;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.B))
        {
            StartCoroutine(Fire());
        }
    }

    IEnumerator Fire()
    {
        yield return new WaitForSeconds(0.2f);
        GameObject bullet = Instantiate(_bulletPrefab, transform.position, transform.rotation);
    }
}
