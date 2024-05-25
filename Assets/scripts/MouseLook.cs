using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private float mouseSens = 10f;

    private float mouseX;
    private float mouseY;
    private float tiltZ;
    private float xRotation = 0f;
    private Vector2 mouseLookVec;

    [SerializeField] private Transform cameraY;
    [SerializeField] private Transform cam;

    [SerializeField] private bool isShip = false;
    private Rigidbody rb;

    private Controls controls;

    private void Awake()
    {
        controls = new Controls();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnLook(InputValue value)
    {
        //mouseLookVec = value.Get<Vector2>().normalized;
        mouseLookVec = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        mouseX = mouseLookVec.x;
        mouseY = mouseLookVec.y;
    }

    private void OnTilt(InputValue value)
    {
        tiltZ = value.Get<float>();
    }

    private void Update()
    {
        if (mouseLookVec.magnitude >= 0.01f || Mathf.Abs(tiltZ) > 0.01f)
        {
            if (isShip)
            {
                Quaternion xRot = Quaternion.Euler(mouseSens * mouseY * Time.deltaTime * new Vector3(-1f, 0f, 0f));
                Quaternion yRot = Quaternion.Euler(mouseSens * mouseX * Time.deltaTime * new Vector3(0f, 1f, 0f));
                Quaternion zRot = Quaternion.Euler(mouseSens * tiltZ * Time.deltaTime * new Vector3(0f, 0f, -1f));
                rb.MoveRotation(rb.rotation * xRot);
                rb.MoveRotation(rb.rotation * yRot);
                rb.MoveRotation(rb.rotation * zRot);
            }
            else
            {
                Debug.Log("looking: " + transform.TransformDirection(transform.up) * (mouseX * mouseSens * Time.deltaTime));

                cameraY.Rotate(new Vector3(0f, 1f, 0f) * (mouseX * mouseSens * Time.deltaTime), Space.Self);
                xRotation -= mouseY * mouseSens * Time.deltaTime;
                xRotation = Mathf.Clamp(xRotation, -85f, 85f);

                cam.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            }
        }
    }
}
