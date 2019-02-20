using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PoliceMovement : MonoBehaviour
{
    public GameManager GM;
    public GameObject PI;//Police Icon
    public GameObject PD;//police distance;
    public float baseLerp = 0.002f;
    public float baseSpeed = 1f;
    public float baseSpeedIncrement = 0.05f;

    float currSpeed;
    float targetPos;

    float lastPos = 0;
    float currPos = 0;
    float disTC;//distance to closest

    GameObject P1;
    GameObject P2;
    // Start is called before the first frame update
    void Start()
    {
        currSpeed = baseSpeed;
        P1 = GM.Player01;
        P2 = GM.Player02;
    }

    // Update is called once per frame
    void Update()
    {
        Calculation();
        Implementation();
        Icon();
    }

    void Icon()
    {
        PI.GetComponent<RectTransform>().anchoredPosition = new Vector3(200-6*(disTC-5), 45,0);
        PD.GetComponent<Text>().text = (Mathf.RoundToInt(disTC-5)).ToString();
    }

    void Calculation()
    {
        currSpeed += baseSpeedIncrement;
        targetPos = GM.Closer+10;

        disTC = GM.Closer - transform.position.x;
        currPos = transform.position.x;
        //print("current speed"+(currPos-lastPos)*Time.deltaTime*100);
        //print("non lerp speed" + currSpeed * Time.deltaTime * 100);
        //print("distance" + disTC);
        lastPos = currPos;
    }

    void Implementation()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(targetPos, 0, 0), baseLerp) + new Vector3(currSpeed * Time.deltaTime, 0,0 );
    }
}
