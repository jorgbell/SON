using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ROOM
{
    OUTSIDE,
    DISCO,
    BATHROOM,
    POOL
}

public class GameManager : MonoBehaviour
{
    private GameManager _instance = null;
    private ROOM _actualRoom;
    
    
    private void Awake()
    {
        //patr�n singleton
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
       
        _actualRoom = ROOM.OUTSIDE;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void changeRoom(ROOM newRoom)
    {
        if (newRoom == _actualRoom)
        {
            Debug.LogError("Est�s indicando que entra y sale de la misma habitaci�n");
            return;
        }
        //resetea valores que indican que SALES de una habitacion
        switch (_actualRoom)
        {
            case ROOM.OUTSIDE:
                
                break;
            case ROOM.DISCO:
                break;
            case ROOM.BATHROOM:
                break;
            case ROOM.POOL:
                break;
        }
        //setea los valores que indican que est�s DENTRO de una habitaci�n
        _actualRoom = newRoom;
        switch (_actualRoom){
            case ROOM.OUTSIDE:
                break;
            case ROOM.DISCO:
                break;
            case ROOM.BATHROOM:
                break;
            case ROOM.POOL:
                break;
        }
    }
}
