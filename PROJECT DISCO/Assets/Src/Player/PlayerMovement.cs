using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Movimiento sencillo usando el CharacterController que proporciona Unity, aplicando gravedad sencilla por si el escenario tiene desniveles.
public class PlayerMovement : MonoBehaviour
{
    public float _speed = 12f;
    private CharacterController _controller;
    Vector3 velocity;
    // Update is called once per frame

    public float _gravityFactor = -9.81f;
    //check para saber si está en el suelo
    public Transform _feet;
    public float _checkSphereRadius = 0.4f;
    public LayerMask _groundMask;
    private bool _isOnGround;

    //SOUND
    private FMODUnity.StudioGlobalParameterTrigger walkGlobalParameter;


    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        walkGlobalParameter = GetComponent<FMODUnity.StudioGlobalParameterTrigger>();
    }
    void Update()
    {

        _isOnGround = Physics.CheckSphere(_feet.position, _checkSphereRadius, _groundMask);
        if (_isOnGround && velocity.y < 0)
            velocity.y = -2f;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        int isWalkingNow = (x != 0 || z != 0) ? 1 : 0;
        //set sound global parameters
        walkGlobalParameter.TriggerParameters(isWalkingNow);


        Vector3 movement = transform.right * x + transform.forward * z;

        _controller.Move(movement * _speed * Time.deltaTime);
        velocity.y += _gravityFactor * Time.deltaTime;
        _controller.Move(velocity * Time.deltaTime); //la formula de la velocidad multiplica por t al cuadrado
    }
}
