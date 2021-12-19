using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BathroomBehaviour : RoomBehaviour
{
    public ObjectsPool npcPool;
    public ActivateAction[] randomEmmiters;
    public float probability = 10f;

    private void Start()
    {
        InvokeRepeating("playRandomSound", 0, 2.5f);
    }
    private void playRandomSound()
    {
        if (!roomEnabled)
            return;

        int random = Random.Range(0, 100);
        Debug.Log(random);
        if (randomEmmiters.Length > 0 && random <= probability)
        {
            int index = Random.Range(0, randomEmmiters.Length - 1);
            randomEmmiters[index].playObjectSound(); //inicia o para uno de los aleatorios
            Debug.Log("Reproduciendo sonido aleatorio");
        }
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
