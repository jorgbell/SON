using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBehaviour : MonoBehaviour
{
    public ROOM thisRoom;
    public ObjectsPool[] _pools;
    //Objetos de esta sala que emiten sonidos y son continuos según estás o no dentro de la misma
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



    public void startSounds()
    {

        foreach(ObjectsPool p in _pools)
        {
            p.Initialize();
        }

    }

    public void stopSounds()
    {
        foreach (ObjectsPool p in _pools)
        {
            p.Stop();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (thisRoom == GameManager._instance.checkRoom())
            return;
        Debug.Log("Entra a " + thisRoom);
        GameManager._instance.changeRoom(thisRoom);
    }

}
