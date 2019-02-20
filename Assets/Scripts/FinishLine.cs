using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    public Color blue;
    public Color yellow;
    bool winner = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            SceneManager.LoadScene("Level00Tutorial");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SceneManager.LoadScene("Level01Australia");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (winner == false)
        {
            winner = true;
            if (other.gameObject.name == "Player01")
            {
                GetComponent<MeshRenderer>().material.color = blue;
            }
            if (other.gameObject.name == "Player02")
            {
                GetComponent<MeshRenderer>().material.color = yellow;
            }
        }
    }
}
