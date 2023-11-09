using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arma : MonoBehaviour
{
    public static Rigidbody2D Gun;

    void Start()
    {
        Gun = GetComponent<Rigidbody2D>();
    }
}
