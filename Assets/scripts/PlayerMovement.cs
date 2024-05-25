using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform testPlanet; //hardcoded
    private AstronomicalBody currentPlanet;

    [SerializeField] private Rigidbody rb;

    [SerializeField] private Transform cam;
    [SerializeField] private Transform cameraY;

    //movement speed
    [SerializeField] private float speed = 2f;
    [SerializeField] private float sprintSpeed = 6f;
    [SerializeField] private float gravRotateSpeed = 40f;

    [SerializeField] private Transform groundChecker;
    [SerializeField] private float groundDistance = 0.2f;
    [SerializeField] private LayerMask groundMask;
    public bool isGrounded = false;
    private bool justJumped = false;

    private float gravity; //needs to be calculated depending on current planet and how far away you are.
    [SerializeField] private float jumpHeight = 3f;
    [SerializeField] private float jumpCooldown = 0.5f;
    private Vector3 velocity;

    [SerializeField] private Transform spaceship;

    private Controls controls;

    //input direction
    private Vector3 direction;

    private void Awake()
    {
        controls = new Controls();

        controls.Player.Jump.performed += ctx => Jump();
        controls.Player.EnterShip.performed += ctx => EnterShip();
    }

    private void Start()
    {
        //test hard coded:
        gravity = -9.81f;

        Cursor.lockState = CursorLockMode.Locked;

        currentPlanet = testPlanet.GetComponent<AstronomicalBody>();

        //TEST HARDCODED
        transform.parent = testPlanet;
    }

    private void OnMovement(InputValue value)
    {
        Debug.Log("oh brother");
        Vector2 dirVector2 = value.Get<Vector2>();
        direction = new Vector3(dirVector2.x, 0f, dirVector2.y).normalized;
    }

    private void Update()
    {
        //Vector3 direction = (testPlanet.transform.position - transform.position).normalized;
        //transform.rotation = Quaternion.Euler(direction);
        //Debug.Log(Quaternion.Euler(direction));
        //transform.LookAt(testPlanet.position);
        //transform.Rotate(new Vector3(-90f, 0f, 0f));

        Vector3 gravUp = (transform.position - testPlanet.position).normalized;
        Vector3 playerUp = transform.up;
        Quaternion targetRot = Quaternion.FromToRotation(playerUp, gravUp) * transform.rotation;
        //transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, gravRotateSpeed * Time.deltaTime);
        transform.rotation = targetRot;

        isGrounded = Physics.CheckSphere(groundChecker.position, groundDistance, groundMask);

        if (isGrounded && !justJumped)
        {
            velocity = -2f * gravUp;
        }
        else
        {
            velocity += gravity * 500f * Time.deltaTime * gravUp;
        }

        rb.velocity = velocity * Time.deltaTime;

        /**
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        Debug.Log(velocity);**/

        if (direction.magnitude >= 0.1f)
        {
            //float targetAngle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg + cameraY.eulerAngles.y;
            Vector3 moveDir = new Vector3(direction.x, 0f, direction.z);
            rb.velocity += speed * 500f * Time.deltaTime * cameraY.TransformDirection(moveDir);

            /**
            Vector3 dirNorm = direction.normalized;
            Vector3 testVector = dirNorm.x * transform.forward + 1f * transform.up + dirNorm.z * transform.right;
            Debug.Log(transform.forward);
            controller.Move(testVector *  Time.deltaTime);**/
        }
    }

    private void FixedUpdate()
    {
        /*float distance = Vector3.Distance(currentPlanet.transform.position, transform.position);
        float accel = (PhysicalLaw.instance.gravConst * currentPlanet.mass) / (distance * distance);
        Vector3 dir = (currentPlanet.transform.position - transform.position).normalized;
        rb.velocity = dir * accel / 30f;*/
    }

    private void Jump()
    {
        Debug.Log("Jumped");
        if (isGrounded)
        {
            StartCoroutine(JustJumped());
            velocity = transform.up * jumpHeight * 500f;
        }
    }

    private IEnumerator JustJumped()
    {
        justJumped = true;

        yield return new WaitForSeconds(jumpCooldown);

        justJumped = false;
    }

    private void EnterShip()
    {
        controls.Player.Disable();
        spaceship.GetComponent<ShipMovement>().enabled = true;
        GetComponent<PlayerInput>().enabled = false;
        spaceship.GetComponent<PlayerInput>().enabled = true;
        cam.GetComponent<Camera>().enabled = false;
        GetComponent<MouseLook>().enabled = false;
        spaceship.GetComponent<MouseLook>().enabled = true;
        gameObject.SetActive(false);
        enabled = false;
    }

    private void OnEnable()
    {
        controls.Spaceship.Disable();
        controls.Player.Enable();

        cam.GetComponent<Camera>().enabled = true;
        GetComponent<MouseLook>().enabled = true;
    }

    private void OnDisable()
    {
        controls.Player.Disable();
        controls.Spaceship.Enable();
    }
}
