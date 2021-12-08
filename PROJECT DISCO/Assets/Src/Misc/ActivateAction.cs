using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateAction : MonoBehaviour
{
    public FMODUnity.StudioEventEmitter actionSound;
    public KeyCode activationKey;

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(activationKey))
        {
            Debug.Log("ACTIVADO");
            //actionSound.Play();
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (Input.GetKeyDown(activationKey))
        {
            Debug.Log("ACTIVADO");

            //actionSound.Play();
        }
    }
}
