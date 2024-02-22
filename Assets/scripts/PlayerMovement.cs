using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform testPlanet; //hardcoded

    [SerializeField] private CharacterController controller;

    [SerializeField] private Transform cam;

    //movement speed
    [SerializeField] private float speed = 2f;
    [SerializeField] private float sprintSpeed = 6f;

    //turning
    [SerializeField] private float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;

    [SerializeField] private Transform groundChecker;
    [SerializeField] private float groundDistance = 0.2f;
    [SerializeField] private LayerMask groundMask;
    public bool isGrounded = false;
    private bool justJumped = false;

    private float gravity; //needs to be calculated depending on current planet and how far away you are.
    [SerializeField] private float jumpHeight = 3f;
    [SerializeField] private float jumpCooldown = 0.5f;
    private Vector3 velocity;

    Controls controls;

    //input direction
    private Vector3 direction;

    private void Awake()
    {
        controls = new Controls();

        controls.Player.Jump.performed += ctx => Jump();
    }

    private void Start()
    {
        //test hard coded:
        gravity = -9.81f;

        Cursor.lockState = CursorLockMode.Locked;
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
        transform.LookAt(testPlanet.position);
        transform.Rotate(new Vector3(-90f, 0f, 0f));

        isGrounded = Physics.CheckSphere(groundChecker.position, groundDistance, groundMask);

        if (isGrounded && !justJumped)
        {
            velocity = -2f * transform.up;
        }
        else
        {
            velocity += gravity * Time.deltaTime * transform.up;
            controller.Move(velocity * Time.deltaTime);
        }

        /**
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        Debug.Log(velocity);**/

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(transform.rotation.x, angle, transform.rotation.z);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(speed * Time.deltaTime * moveDir.normalized);

            /**
            Vector3 dirNorm = direction.normalized;
            Vector3 testVector = dirNorm.x * transform.forward + 1f * transform.up + dirNorm.z * transform.right;
            Debug.Log(transform.forward);
            controller.Move(testVector *  Time.deltaTime);**/
        }
    }

    private void Jump()
    {
        Debug.Log("Jumped");
        if (isGrounded)
        {
            StartCoroutine(JustJumped());
            velocity = transform.up * jumpHeight;
        }
    }

    private IEnumerator JustJumped()
    {
        justJumped = true;

        yield return new WaitForSeconds(jumpCooldown);

        justJumped = false;
    }

    private void OnEnable()
    {
        controls.Player.Enable();
    }

    private void OnDisable()
    {
        controls.Player.Disable();
    }
}