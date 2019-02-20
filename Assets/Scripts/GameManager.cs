using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject Player01;
    public GameObject Player02;

    public float Farther;
    public float Closer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Player01.GetComponent<Transform>().position.x < Player02.GetComponent<Transform>().position.x)
        {
            Closer = Player01.GetComponent<Transform>().position.x;
            Farther = Player02.GetComponent<Transform>().position.x;
        }
        else
        {
            Closer = Player02.GetComponent<Transform>().position.x;
            Farther = Player01.GetComponent<Transform>().position.x;
        }
    }

}
