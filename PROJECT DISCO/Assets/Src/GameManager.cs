using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager _instance = null;
    private ROOM _actualRoom = ROOM._MAX; //inicializamos vacío
    public RoomBehaviour[] rooms = new RoomBehaviour[(int)ROOM._MAX];
    public FMODUnity.StudioGlobalParameterTrigger roomTrigger;
    public PlayerMovement player;
    private void Awake()
    {        
        //patrón singleton
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        if (rooms.Length != (int)ROOM._MAX)
            Debug.LogError("NECESITAS PONER MÁS/MENOS HABITACIONES O MODIFICAR EL ENUM");

        //inicializamos el juego en las afueras
        changeRoom(ROOM.OUTSIDE);
    }

    public void changeRoom(ROOM newRoom)
    {
        
        if (newRoom == _actualRoom)
        {
            Debug.LogWarning("Estás indicando que entra y sale de la misma habitación");
            return;
        }
        if(_actualRoom != ROOM._MAX) //En caso de que no esté aún en ninguna sala, no hay que desactivar ninguna
        {
            //le indica a los sonidos de esa sala que paren de emitir todos los eventos
            rooms[(int)_actualRoom].disable();
        }
        //le indica a los sonidos de esa sala que empiecen a emnitir los eventos
        _actualRoom = newRoom;
        roomTrigger.TriggerParameters((int)_actualRoom);
        rooms[(int)_actualRoom].enable();
        

        Debug.Log(_actualRoom);

    }

    public ROOM checkRoom() { return _actualRoom; }

    public Transform getPlayer() { return player.transform; }
}
