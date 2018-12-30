using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour {

    [Tooltip("In Seconds")][SerializeField] float levelLoadDelay = 1f;
    [Tooltip("FX prefab on Player")][SerializeField] GameObject deathFX;

	void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag != "EnvTrigger")
        {
            StartDeathSequence();
            deathFX.SetActive(true);
            Invoke("ReloadScene", levelLoadDelay);
        }
       
        
    }

    private void StartDeathSequence()
    {
        SendMessage("OnPlayerDeath");
       
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(1);
    }

    
}
