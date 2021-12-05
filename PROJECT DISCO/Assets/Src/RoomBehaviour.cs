using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBehaviour : MonoBehaviour
{
    [System.Serializable]
    public struct Emitter
    {
        public FMODUnity.StudioEventEmitter _emitter;
        public bool fadeOut;
    }
    //Objetos de esta sala que emiten sonidos y son continuos seg�n est�s o no dentro de la misma
    public Emitter[] roomEmitters;
    protected bool roomEnabled;
    public bool isEnabled() { return roomEnabled; }

    public virtual void enable()
    {
        roomEnabled = true;
        startSounds();
    }
    public virtual void disable()
    {
        roomEnabled = false;
        stopSounds();
    }

    //OVERRIDE
    public virtual void logic()
    {
        ;
    }


    public void startSounds()
    {
        foreach(Emitter e in roomEmitters)
        {
            //ENCIENDE todos los eventos asociados
            e._emitter.Play();
        }
    }

    public void stopSounds()
    {
        foreach (Emitter e in roomEmitters)
        {
            //APAGA todos los eventos asociados
            if (e.fadeOut)
            {
                StartCoroutine(fader(e));
            }
            else
                e._emitter.Stop();
        }
    }
    
    IEnumerator fader(Emitter e)
    {
        e._emitter.SetParameter("Fade", 0);
        yield return new WaitForSeconds(3);
        e._emitter.Stop();
        yield return null;

    }


}