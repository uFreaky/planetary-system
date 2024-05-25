using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShipMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Camera cam;
    [SerializeField] private Transform player;

    [SerializeField] private float speed = 6f;

    //input direction
    private Vector3 direction;

    private Controls controls;

    private void Awake()
    {
        controls = new Controls();

        controls.Spaceship.ExitShip.performed += ctx => ExitShip();
    }

    private void OnMovement(InputValue value)
    {
        Debug.Log("oh brother");
        Vector3 dirVector3 = value.Get<Vector3>();
        direction = new Vector3(dirVector3.x, dirVector3.y, dirVector3.z).normalized;
    }

    private void Update()
    {
        if (direction.magnitude >= 0.1f)
        {
            //Vector3 moveDir = new Vector3(direction.x, direction.y, direction.z);
            rb.velocity += speed * Time.deltaTime * transform.TransformDirection(direction);
        }
    }

    private void ExitShip()
    {
        controls.Spaceship.Disable();

        cam.enabled = false;
        GetComponent<PlayerInput>().enabled = false;
        player.gameObject.SetActive(true);
        player.GetComponent<PlayerInput>().enabled = true;
        player.GetComponent<PlayerMovement>().enabled = true;
        player.GetComponent<MouseLook>().enabled = true;
        GetComponent<MouseLook>().enabled = false;

        enabled = false;
    }

    private void OnEnable()
    {
        controls.Player.Disable();
        controls.Spaceship.Enable();

        cam.enabled = true;
    }

    private void OnDisable()
    {
        controls.Spaceship.Disable();
        controls.Player.Enable();
    }
}
