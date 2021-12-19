using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Emitter
{
    public FMODUnity.StudioEventEmitter emmiter;
    public bool fadeOut;
}
public class SoundEmitterObject : MonoBehaviour
{
    public Emitter _soundEmitter;
    public virtual void setInitialState()
    {
        ;
    }

    public void playSound()
    {
        if(_soundEmitter.emmiter.enabled ==true)
            _soundEmitter.emmiter.Play();

    }

    public void stopSound()
    {
        if (_soundEmitter.emmiter.enabled == false)
            return;

        if (_soundEmitter.fadeOut)
        {
            StartCoroutine(fader(_soundEmitter.emmiter));
        }
        else
            _soundEmitter.emmiter.Stop();
    }


    IEnumerator fader(FMODUnity.StudioEventEmitter e)
    {
        e.SetParameter("Fade", 0);
        yield return new WaitForSeconds(3);
        e.Stop();
        yield return null;

    }

}
