using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public bool test = false;

    public GameManager GM;
    public float minZoomDistance;
    public float maxZoomDistance;
    public float lerpSpeed = 0.03f;
    public float velocityZoom = 0.2f;
    public float playerDistance;

    float targetZ = 0;
    float targetX = 0;
    float targetY = 0;
    float lower;
    float higher;
    float closer;
    float further;
    Vector3 targetPos;

    GameObject Player01;
    GameObject Player02;

    Transform P1;
    Transform P2;
    GameObject faster;

    void Start()
    {
        Player01 = GM.Player01;
        Player02 = GM.Player02;
        P1 = Player01.GetComponent<Transform>();
        P2 = Player02.GetComponent<Transform>();
        playerDistance = Vector3.Distance(P1.position, P2.position);
    }

    
    void FixedUpdate()
    {
        if (!test)
        {
            Calculation();
            Implementation();
        }
        else
        {
            targetPos = new Vector3((P1.position.x + P2.position.x)/2, -2, -290);
            GetComponent<Transform>().position = Vector3.Lerp(GetComponent<Transform>().position, targetPos, 0.1f);
        }
    }

    void Calculation()
    {
        //find out faster, further, closer, higher, lower
        if (P1.position.x > P2.position.x)
        {
            further = P1.position.x;
            closer = P2.position.x;
        }
        else
        {
            further = P2.position.x;
            closer = P1.position.x;
        }
        if (P1.position.y > P2.position.y)
        {
            higher = P1.position.y;
            lower = P2.position.y;
        }
        else
        {
            higher = P2.position.y;
            lower = P1.position.y;
        }
        if (Mathf.Abs(P1.GetComponent<Rigidbody>().velocity.x) > Mathf.Abs(P1.GetComponent<Rigidbody>().velocity.x))
            faster = P1.gameObject;
        else
            faster = P2.gameObject;

        //set targetZ
        playerDistance = Mathf.Abs(P1.position.x-P2.position.x); 
        targetZ = 3 * Mathf.Clamp(-playerDistance * 1.5f, minZoomDistance, maxZoomDistance) - Mathf.Abs(faster.gameObject.GetComponent<Rigidbody>().velocity.x) * velocityZoom;

        //set targetX Y
        if (playerDistance > -maxZoomDistance / 1.5f)
        {
            targetX = further;
            targetY = lower + Mathf.Abs(P1.position.y - P2.position.y) * 0.5f + 2;
        }
        else
        { 
            targetX = (P1.position.x + P2.position.x) / 2 + (-maxZoomDistance / 3) + (Mathf.Abs(-maxZoomDistance / 1.5f - playerDistance + 0.1f) / 3);
            targetY = lower + Mathf.Abs(P1.position.y - P2.position.y) * 0.5f + 2;
        }
        targetY = Mathf.Clamp(targetY, -12, 500);

        //set targetPos
        targetPos = new Vector3(targetX, targetY, targetZ);
    }

    void Implementation()
    {
        GetComponent<Transform>().position = Vector3.Lerp(GetComponent<Transform>().position, targetPos, lerpSpeed);
    }
}
