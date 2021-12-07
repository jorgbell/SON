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
        float poolDistance = Vector3.Distance(player.position, poolDoor.position);
        float bathroomDistance = Vector3.Distance(player.position, bathroomCenter.position);

        switch (GameManager._instance.checkRoom())
        {
            case ROOM.OUTSIDE:
                discoPlaylist.SetParameter("OutsideDistance", outsideDistance);
                break;
            case ROOM.BATHROOM:
                int factor = checkBathroomPosition();
                discoPlaylist.SetParameter("BathroomDistance", bathroomDistance*factor);
                break;
            case ROOM.POOL:
                discoPlaylist.SetParameter("PoolDistance", poolDistance);
                Debug.Log(poolDistance);
                break;
            case ROOM.DISCO:
                break;
            default:
                break;
        }


    }

    private int checkBathroomPosition()
    {
        int res = 1;
        if (player.position.z <= bathroomCenter.position.z) //esta mas cerca de la puerta?
            res = -1;
        return res;
    }



    public override void enable()
    {
        base.enable();
        discoPlaylist.SetParameter("OutsideDistance", 0); //seteamos al máximo de cercanía la distancia a la discoteca para evitar problemas
        discoPlaylist.SetParameter("BathroomDistance", -20); //lo mismo para el baño
        discoPlaylist.SetParameter("PoolDistance", 0); //lo mismo para el baño

    }

    public override void disable()
    {
        base.disable();
    }
}
