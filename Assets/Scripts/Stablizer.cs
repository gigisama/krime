using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stablizer : MonoBehaviour
{
    public GameObject PC;//player core

    Rigidbody PCrb;//player core rb;

    // Start is called before the first frame update
    void Start()
    {
        PCrb = PC.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0,0,-PCrb.rotation.z);
    }
}
