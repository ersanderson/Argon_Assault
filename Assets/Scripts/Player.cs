using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


public class Player : MonoBehaviour {

    [Tooltip("In ms^-1")][SerializeField] float xSpeed = 20f;
    [Tooltip("In m")] [SerializeField] float xRange = 10.5f;

    [Tooltip("In ms^-1")] [SerializeField] float ySpeed = 4f;
    [Tooltip("In m")] [SerializeField] float yRange = 5f;

    [SerializeField] float positionPitchFactor = -3f;
    [SerializeField] float controlPitchFactor = -5f;

    [SerializeField] float positionYawFactor = 3f;
   
    [SerializeField] float controlRollFactor = -13f;


    float xThrow, yThrow;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        ProcessTranslation();
        ProcessRotation();
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        print("Player Collided Something");
    }

    private void OnTriggerEnter(Collider other)
    {
        print("Player Triggered Something");
    }


    private void ProcessTranslation()
    {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");

        float xOffset = xThrow * xSpeed * Time.deltaTime;
        float yOffset = yThrow * ySpeed * Time.deltaTime;

        float rawXPos = transform.localPosition.x + xOffset;
        float rawYPos = transform.localPosition.y + yOffset;

        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }


    private void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrow * controlPitchFactor;

        float yawDueToPosition = transform.localPosition.x * positionYawFactor;
        
        float pitch = pitchDueToPosition + pitchDueToControlThrow;
        float yaw = yawDueToPosition;
        float roll = xThrow * controlRollFactor;

        transform.localRotation = Quaternion.Euler( pitch, yaw , roll ); 
    }




}



