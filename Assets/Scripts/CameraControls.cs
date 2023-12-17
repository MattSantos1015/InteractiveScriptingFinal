using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Transform orientation;
    float xRotation;
    float yRotation;
    public float LookSensitivity;

    public Transform cameraPosition;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        transform.position = cameraPosition.position;

        //CameraRotation
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * LookSensitivity;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * LookSensitivity;

        yRotation += mouseX;
        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
