using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


public class PlayerController : MonoBehaviour {

    [Header("General")]
    [Tooltip("In ms^-1")][SerializeField] float controlSpeed = 20f;
    [Tooltip("In m")] [SerializeField] float xRange = 10.5f;
    [Tooltip("In m")] [SerializeField] float yRange = 5f;
    [SerializeField] GameObject[] guns;
    

    [Header("Screen Position Parameters")]
    [SerializeField] float positionPitchFactor = -3f;
    [SerializeField] float positionYawFactor = 3f;

    [Header("Controller Throw Parameters")]  
    [SerializeField] float controlPitchFactor = -5f;
    [SerializeField] float controlRollFactor = -13f;
   
    float xThrow, yThrow;
    bool isControlEnabled = true;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (isControlEnabled)
        {
            ProcessTranslation();
            ProcessRotation();
            ProcessFiring();
        }
                
    }

    // called by string message from Collision Handler
    public void OnPlayerDeath()
    {
        
        isControlEnabled = false;
        
    }


    private void ProcessTranslation()
    {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");

        float xOffset = xThrow * controlSpeed * Time.deltaTime;
        float yOffset = yThrow * controlSpeed * Time.deltaTime;

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

    void ProcessFiring()
    {
        if (CrossPlatformInputManager.GetButton("Fire"))
        {
            SetGunsActive(true);
        }
        else
        {
            SetGunsActive(false);
        }
    }

    private void SetGunsActive(bool isActive)
    {
        foreach(GameObject gun in guns)
        {           
            var emissionModule = gun.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isActive;          
        }
    }

}



