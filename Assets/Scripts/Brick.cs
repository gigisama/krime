using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    Rigidbody rb;
    ParticleSystem PS;
    float lastVelo;
    float currVelo;
    float basePos;

    // Start is called before the first frame update
    void Start()
    {
        PS = GetComponent<ParticleSystem>();
        rb = GetComponent<Rigidbody>();
        basePos = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        currVelo = rb.velocity.y;
        if (Mathf.Abs(basePos-transform.position.x)>2&&lastVelo * currVelo < 0)
            PS.Emit(3);
        lastVelo = currVelo;
    }
}
