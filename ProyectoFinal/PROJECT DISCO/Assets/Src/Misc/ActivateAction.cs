using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateAction : MonoBehaviour
{
    public FMODUnity.StudioEventEmitter actionSound;
    public KeyCode activationKey;
    public bool loop = false;
    private bool isPlaying = false;

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(activationKey))
            playObjectSound();
    }

    public void playObjectSound()
    {
        if (loop && isPlaying)
        {
            Debug.Log("DESACTIVADO");
            actionSound.Stop();
            isPlaying = false;
        }
        else
        {
            Debug.Log("ACTIVADO");
            actionSound.Play();
            isPlaying = true;
        }
    }
}
