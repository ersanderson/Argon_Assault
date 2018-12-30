using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityEnable : MonoBehaviour {

    ParticleSystem particle;

    void Start()
    {
        particle = GetComponent<ParticleSystem>();
        particle.Stop();
    }

    void OnTriggerEnter(Collider other)
    {
        particle = GetComponent<ParticleSystem>();
        particle.Play();
        print("Ive Entered");

    }

    void OnTriggerExit(Collider other)
    {
        particle = GetComponent<ParticleSystem>();
        particle.Stop();
        print("Ive Exited");
    }
    


}
