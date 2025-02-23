using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        // Correct the class name here to match the actual name in your PlayerController script
        PlayerControllerTutorialUpdates controller = other.GetComponent<PlayerControllerTutorialUpdates>();
        if (controller != null)
        {
            controller.ChangeHealth(1);
            Destroy(gameObject);
        }
    }
}