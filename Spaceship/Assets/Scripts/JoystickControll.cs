using System;
using UnityEngine;

public class JoystickControll : MonoBehaviour
{   //public GameObject spaceship;
    public GameManager gameManager;
    public Rigidbody spaceship;


    public Transform topOfJoystick;
    public Quaternion originalJoystick;
    private float forwardBackwardTiltMax;
    
    public float forwardBackwardTilt = 0;
    private float forwardBackwardTiltNormalnized;

    [SerializeField]
    private float sideToSideTilt = 0;
    
    private void Start()
    {   //get spaceship rigibody
        spaceship = gameManager.spaceship.GetComponent<Rigidbody>();
        //
        forwardBackwardTiltMax = 17f;
        originalJoystick = transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {

        forwardBackwardTilt = topOfJoystick.rotation.eulerAngles.x;
        forwardBackwardTiltNormalnized = map(forwardBackwardTilt, 0f,forwardBackwardTiltMax,0f,1f);
        print(forwardBackwardTiltNormalnized);
        if (forwardBackwardTilt < 355 && forwardBackwardTilt > 290)
        {
            forwardBackwardTilt = Math.Abs(forwardBackwardTilt - 360);
            Debug.Log("Backward" + forwardBackwardTilt);
            //Move something using forwardBackwardTilt as speed
        }
        else if (forwardBackwardTilt > 5 && forwardBackwardTilt < 74)
        {
            Debug.Log("Forward" + forwardBackwardTilt);
            //Move something using forwardBackwardTilt as speed

        }
        if(forwardBackwardTilt > forwardBackwardTiltMax && forwardBackwardTilt < 74)
        {
            transform.localRotation = originalJoystick;
        }
        

        sideToSideTilt = topOfJoystick.rotation.eulerAngles.z;
        if (sideToSideTilt < 355 && sideToSideTilt > 290)
        {
            sideToSideTilt = Math.Abs(sideToSideTilt - 360);
            Debug.Log("Right" + sideToSideTilt);
            //Turn something using sideToSideTilt as speed
        }
        else if (sideToSideTilt > 5 && sideToSideTilt < 74)
        {
            Debug.Log("Left" + sideToSideTilt);
            //Turn something using sideToSideTilt as speed
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("PlayerHand"))
        {
            transform.LookAt(other.transform.position, transform.up);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PlayerHand"))
        {
            //transform.LookAt(other.transform.position, transform.up);
            //topOfJoystick.rotation.eulerAngles.x = 0f;
            transform.localRotation = originalJoystick;
            //Debug.Log("exit");
        }
    }

    //Remap range
    float map(float s, float a1, float a2, float b1, float b2)
    {
        return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
    }
}
