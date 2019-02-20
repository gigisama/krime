using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLimbs : MonoBehaviour
{
    public int limbType;//1 head 2 hand 3 feet
    public GameObject PlayerCore;

    float targetFRotation;//target feet rotation;
    PlayerMovement pm;
    HingeJoint hj;
    JointSpring sj;
    float tarBRotation;//target Body rotation
    KeyCode jump;
       
    
    void Start()
    {
        pm = PlayerCore.GetComponent<PlayerMovement>();
        jump = pm.jump;
        tarBRotation = pm.targetRotation;
        hj = GetComponent < HingeJoint > ();
    }

   
    void Update()
    {
        switch (limbType)
        {
            case 1:
                tarBRotation = pm.targetRotation;
                targetFRotation = -tarBRotation;
                break;
            case 2:
                tarBRotation = pm.targetRotation;
                targetFRotation = -tarBRotation;
                break;
            case 3:
                tarBRotation = pm.targetRotation;
                targetFRotation = -tarBRotation;
                if (Input.GetKey(jump))
                    targetFRotation -= 100;
                break;
        }

        sj.targetPosition = targetFRotation;
        sj.damper = 0f;
        sj.spring = 0.1f;

        hj.spring = sj;
    }
}
