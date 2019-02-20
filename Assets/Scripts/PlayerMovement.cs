using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float encourageScaler = 1f;

    public KeyCode jump;
    public KeyCode turnRight;
    public KeyCode turnLeft;
    public KeyCode brake;

    public float jumpForce = 1;
    public float turnForce = 1;
    public float turnSpeed = 1;
    public float brakeForce = 1;
    public float jumpingTime = 0.4f;
    public float airBoost = 2f;

    public float targetRotation = 0;
    float currRotation = 0;
    float actualTF;
    float actualJF;
    float actualBF;

    Rigidbody rb;
    float maxRotation = 49;
    float jumpGracefulTime = 0.17f;
    float jumpGTimer;
    float groundGracefulTime = 0.02f;
    float groundGTimer;
    float drag;
    public bool jumpPressed = false;
    public bool grounded = false;
    public Image RocketBar;
    bool jumping = false;
    float jumpingTimer;
    float currVelo = 0;
    ParticleSystem ps;
    bool toRight = true;
    bool lastToright = true;
    bool rocketActivated = false;
    bool test = false;
    public bool trapped = false;


    void Start()
    {
        jumpingTimer = jumpingTime;
        rb = GetComponent<Rigidbody>();
        drag = rb.drag;
        ps = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
       
        PlayerJump();
        PlayerRotation();
        PlayerBrake();
        PlayerAirBoost();

        //for debugging 

        if (Input.GetKeyDown(KeyCode.Space))
            test = !test;
        if (test)
            jumpingTimer = jumpingTime;

    }

    void PlayerAirBoost()
    {
        //air boost
        if (!grounded && Input.GetKeyDown(jump))
        {
            rocketActivated = true;
        }
        if (rocketActivated && jumpingTimer > 0 && Input.GetKey(jump))
        {
            jumpingTimer -= Time.deltaTime;
            ps.Emit(2);
            rb.AddForce(transform.up * actualJF * airBoost * Time.deltaTime, ForceMode.VelocityChange);
        }
        else if(jumpingTimer < jumpingTime && !Input.GetKey(jump))
        {
            jumpingTimer += Time.deltaTime * 0.1f;
            if (grounded)
            {
                jumpingTimer += Time.deltaTime * 0.3f;
                if (jumpingTimer > jumpingTime * 0.6f)
                    jumpingTimer = jumpingTime;
            }
        }

        RocketBar.fillAmount = jumpingTimer / jumpingTime;
            /*old air boost////////////////////////////////////////////////////////////////////////////////
             * jump time = 0.55
             * air boost = 1.7
             * if (!grounded && Input.GetKey(jump))
            {
                jumpingTimer -= Time.deltaTime / encourageScaler;
                if (jumpingTimer > 0)
                {
                    ps.Emit(2);
                    rb.AddForce(transform.up * actualJF * airBoost * Time.deltaTime, ForceMode.VelocityChange);
                }
            }*/
        }

    void PlayerRotation()
    {
        //set curr dirction to Right??
        currVelo = rb.velocity.x;
        if (Mathf.Abs(currVelo) > 5f)
            toRight = currVelo > 0;
        if (lastToright != toRight)
        {
            transform.Rotate(Vector3.up * 180);
            targetRotation *= -1;
        }
        lastToright = toRight;

        //get correct usable current rotation
        currRotation = transform.rotation.eulerAngles.z;
        if (currRotation > 180)
            currRotation -= 360;

        //set target rotation
        if (toRight)
        {
            if (Input.GetKey(turnRight))
                targetRotation -= Time.deltaTime * turnSpeed;
            else if (Input.GetKey(turnLeft))
                targetRotation += Time.deltaTime * turnSpeed;
            targetRotation = Mathf.Clamp(targetRotation, -maxRotation, maxRotation);
        }
        else
        {
            if (Input.GetKey(turnLeft))
                targetRotation -= Time.deltaTime * turnSpeed;
            else if (Input.GetKey(turnRight))
                targetRotation += Time.deltaTime * turnSpeed;
            targetRotation = Mathf.Clamp(targetRotation, -maxRotation, maxRotation);
        }

        //actual rotate to target rotation
        if (toRight)
            rb.AddTorque(0, 0, turnForce * (targetRotation - currRotation));
        else
            rb.AddTorque(0, 0, turnForce * (-targetRotation + currRotation));
    }

    void PlayerJump()
    {
        //set actual jump force
        actualJF = jumpForce * encourageScaler * encourageScaler * encourageScaler;
        if (Mathf.Abs(currRotation) < 30)
        {
            if (Mathf.Abs(currRotation) > 15)
                actualJF += (30 - Mathf.Abs(currRotation)) / 6;
            else
                actualJF += 2.5f;
        }

        //set jumppressed
        if (Input.GetKeyDown(jump))
        {
            jumpPressed = true;
            jumpGTimer = jumpGracefulTime;
        }
        if (jumpPressed)
        {
            jumpGTimer -= Time.deltaTime;
            if (jumpGTimer < 0)
                jumpPressed = false;
        }

        //set grounded
        if (grounded)
        {
            groundGTimer -= Time.deltaTime;
            if (groundGTimer < 0)
                grounded = false;
        }

        //actual jump
        if (jumpPressed && grounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, 0);
            rb.AddForce(transform.up * actualJF, ForceMode.VelocityChange);
            jumpPressed = false;
            grounded = false;
            jumping = true;
        }
    }

    void PlayerBrake()
    {
        actualBF = brakeForce;

        //actual braking
        if (Input.GetKey(brake) && grounded)
            rb.drag = brakeForce;
        else if (!grounded)
            rb.drag = drag;

        if (trapped)
            rb.drag = drag * 53;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag != "NotGround")
        {
            grounded = true;
            rocketActivated = false;
            groundGTimer = groundGracefulTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag!="NotGround")
        grounded = true;
        //jumpingTimer = jumpingTime ;
    }
}
