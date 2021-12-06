using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscoBehaviour : RoomBehaviour
{
    //para triggerear los parametros que ponen efectos a la playlist
    public Transform outsideDoor;
    public Transform bathroomCenter;
    public Transform poolDoor;

    public FMODUnity.StudioEventEmitter discoPlaylist;

    private Transform player;
    private void Start()
    {
        player = GameManager._instance.getPlayer();
    }

    private void Update()
    {

        float outsideDistance = Vector3.Distance(player.position, outsideDoor.position);
        //float poolDistance = Vector3.Distance(player.position, poolDoor.position);
        //float bathroomDistance = Vector3.Distance(player.position, bathroomCenter.position);

        switch (GameManager._instance.checkRoom())
        {
            case ROOM.OUTSIDE:
                discoPlaylist.SetParameter("OutsideDistance", outsideDistance);
                break;
            case ROOM.DISCO:

                break;
            case ROOM.BATHROOM:
                //discoPlaylist.SetParameter("BathroomDistance", bathroomDistance);
                break;
            case ROOM.POOL:
                //discoPlaylist.SetParameter("PoolDistance", poolDistance);
                break;
        }


        Debug.Log(outsideDistance);
    }


    public override void enable()
    {
        base.enable();
    }

    public override void disable()
    {
        base.disable();
    }
}
