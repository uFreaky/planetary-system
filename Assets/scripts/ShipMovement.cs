using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShipMovement : MonoBehaviour
{
    public Rigidbody rb;
    [SerializeField] private Camera cam;
    [SerializeField] private Transform player;
    private Rigidbody playerRb;

    [SerializeField] private Transform playerSpawn;

    [SerializeField] private float speed = 6f;

    //input direction
    private Vector3 direction;

    private Controls controls;

    private void Awake()
    {
        controls = new Controls();

        controls.Spaceship.ExitShip.performed += ctx => ExitShip();
    }

    private void Start()
    {
        playerRb = player.GetComponent<Rigidbody>();
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
        playerRb.position = transform.position;
        player.position = transform.position;

        Vector3 gravUp = (player.transform.position - transform.parent.position).normalized;
        Vector3 playerUp = player.transform.up;
        Quaternion targetRot = Quaternion.FromToRotation(playerUp, gravUp) * player.transform.rotation;
        player.transform.rotation = targetRot;

        /**
        Vector3 targetPostition1 = transform.position + transform.forward;
        targetPostition1.y = player.position.y;

        Vector3 targetPostition = new Vector3(player.transform.position.x,
                                       player.transform.position.y,
                                       player.transform.position.x);
        player.transform.LookAt(targetPostition1);
        //playerRb.*/
    }

    private void ExitShip()
    {
        controls.Spaceship.Disable();

        cam.enabled = false;
        GetComponent<PlayerInput>().enabled = false;
        player.gameObject.SetActive(true);
        player.GetComponent<PlayerInput>().enabled = true;
        player.GetComponent<PlayerMovement>().enabled = true;
        player.GetComponentInChildren<MouseLook>().enabled = true;
        GetComponent<MouseLook>().enabled = false;
        playerRb.position = playerSpawn.position;
        GetComponent<PlanetDetailUI>().enabled = false;
        Destroy(rb);
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
