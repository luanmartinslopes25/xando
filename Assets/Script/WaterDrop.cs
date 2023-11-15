using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDrop : MonoBehaviour
{
    private Vector3 subtractSize = new Vector3(0.032f, 0.032f, 0);

    // Update is called once per frame
    void Update()
    {
        transform.localScale = transform.localScale - subtractSize * Time.deltaTime;
        if(transform.localScale.y < 0)
        {
            Destroy(gameObject);
        }
    }
}
