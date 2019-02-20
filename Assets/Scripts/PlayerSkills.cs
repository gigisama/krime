using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkills : MonoBehaviour
{
    public float diffRR = 0.1f;//diffR requirement
    public KeyCode defaultSkill;
    public bool encouraging = false;

    public GameObject AnP;//another player
    PlayerMovement AnPM;//another player movement
    PlayerSkills AnPS;//another player skills

    public GameObject IHorn;
    public ParticleSystem IHornedPS;
    ParticleSystem.EmissionModule IHornedPSE;
    ParticleSystem.MainModule IHornedPSM;
    Rigidbody rb;

    public float encourageScalerMax = 1.3f;
    public float encourageSclaerBase = 1.1f;
    public float encourageScaler = 1;
    float lastES;
    float currES;

    float lastPress;
    float currPress;
    float pressTimer;
    float cancleTime = 1.2f;
    float cancleTimer;
    float diff;///
    float diffR;// = diff/currPress - lastPress
    float[] meter = { 1, 1, 0.5f, 0.5f , 1 , 0.5f, 0.5f, 0.5f, 1, 0.5f };
    float fMeter;//first meter
    int noteCount = 0;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        IHornedPSE = IHornedPS.emission;
        IHornedPSM = IHornedPS.main;
        AnPM = AnP.GetComponent<PlayerMovement>();
        AnPS = AnP.GetComponent<PlayerSkills>();
    }

    void Update()
    {
        //horned particle effect
        if (AnPS.encourageScaler != 1)
        {
            IHornedPSE.enabled = true;
            IHornedPSM.startColor = Color.Lerp(Color.yellow, Color.red, (AnPS.encourageScaler-encourageSclaerBase)*(1/(encourageScalerMax-encourageSclaerBase)));
        }
        else
            IHornedPSE.enabled = false;


        if (encouraging)
        {
            
            pressTimer += Time.deltaTime;
            cancleTimer -= Time.deltaTime;

            //set up usable difference ratio
            if (Input.GetKeyDown(defaultSkill))
            {
                lastPress = currPress;
                cancleTimer = cancleTime;
                currPress = pressTimer;

                if (noteCount == meter.Length)
                    noteCount = 0;
                else
                {
                    if (noteCount == 0)
                        fMeter = currPress - lastPress;
                    else
                    {
                        diff = Mathf.Abs((fMeter * meter[noteCount]) - (currPress - lastPress));//standard meter - actual meter = (fMeter * meter[noteCount]) - (currPress - lastPress)
                        diffR = diff / (currPress - lastPress);
                        //print(diffR);
                    }
                    noteCount++;
                }

                //modify encouragScaler according to diffR
                if(diffRR > diffR)
                    currES += (diffRR - diffR)/3;
                else
                    currES += (diffRR - diffR)/1.7f;
            }
            currES -= Time.deltaTime / 4;
            currES = Mathf.Clamp(currES, encourageSclaerBase, encourageScalerMax);
            encourageScaler = Mathf.Lerp(lastES, currES, 0.04f);
            lastES = currES;
            //print(currES);

            if (cancleTimer < 0 || Mathf.Abs(rb.velocity.x)>1)
            {
                encouraging = false;
                encourageScaler = 1;
            }
        }
        else if (Input.GetKeyDown(defaultSkill) && Mathf.Abs(rb.velocity.x) < 1)
        {
            currES = encourageSclaerBase;
            lastES = encourageSclaerBase;
            encouraging = true;
            cancleTimer = cancleTime;
            pressTimer = 0;
            currPress = 0;
            lastPress = 0;
            noteCount = 0;
        }
        
        //apply encourage scaler
        AnPM.encourageScaler = encourageScaler;
    }
}
