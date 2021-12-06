using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsPool : MonoBehaviour
{
    private SoundEmitterObject[] objs;
    private int size;

    private void Awake()
    {
        size = this.transform.childCount;
        objs = new SoundEmitterObject[size];

        for(int i = 0; i<size; i++)
        {
            var child = transform.GetChild(i).GetComponent<SoundEmitterObject>();
            if (child != null)
                objs[i] = child;
        }
    }


    public void Initialize()
    {
        foreach(SoundEmitterObject obj in objs)
        {
            obj.playSound();        //sound
            obj.setInitialState(); //params
        }
    }

    public void Stop()
    {
        foreach (SoundEmitterObject obj in objs)
        {
            obj.stopSound();
        }
    }

    public FMODUnity.StudioEventEmitter getSound(int index)
    {
        FMODUnity.StudioEventEmitter result = null;

        result = objs[index]._soundEmitter.emmiter;

        return result;
    }
}
