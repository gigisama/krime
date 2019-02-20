using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighGrass : MonoBehaviour
{
    void Start()
    {
        
    }


    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerMovement>().trapped = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerMovement>().trapped = false;
        }
    }
}
