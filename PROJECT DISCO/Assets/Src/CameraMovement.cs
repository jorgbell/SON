using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float _sensibility = 100f;
    private float _previousRotation = 0f;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;   
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * _sensibility * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * _sensibility * Time.deltaTime;

        _previousRotation -= mouseY;
        _previousRotation = Mathf.Clamp(_previousRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(_previousRotation, 0f, 0f);
        transform.parent.Rotate(Vector3.up * mouseX); //si movemos el raton de izq a derecha queremos que el eje Y del cilindro rote
        
        
    }
}
