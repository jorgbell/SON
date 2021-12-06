using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum NPCSTATE
{
    NONE,
    PARTY,
    TALK,
    FIGHT,
    _MAX //usar MAX quiere decir que todo esté a 0
}

public class NPCBehaviour : SoundEmitterObject
{
    //variables que indican como se inicializa
    [SerializeField]
    private bool smokes = false;
    [SerializeField]
    private bool drinks = false;
    public NPCSTATE initialState = NPCSTATE.NONE;
    private NPCSTATE actualState;

    //sonido
    public FMODUnity.StudioEventEmitter soundEmitter;

    
    public override void setInitialState()
    {
        //inicializa el estado
        changeState(initialState);
        smokinkgState(smokes);
        drinkingState(drinks);
    }


    public void changeState(NPCSTATE state)
    {
        if(state != NPCSTATE._MAX)
            soundEmitter.SetParameter("NPCState", (int)state);
        actualState = state;
    }

    public void smokinkgState(bool smoking)
    {
        int param = (smoking) ? 1 : 0;
        soundEmitter.SetParameter("Smokes", param);
    }

    public void drinkingState(bool drinks)
    {
        int param = (drinks) ? 1 : 0;
        soundEmitter.SetParameter("Drinks", param);
    }

    public NPCSTATE checkActualState() { return actualState; }
}
