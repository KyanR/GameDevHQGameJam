using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 10;
    public float gravity = -10;
    public float jumpHeight = 3f;
    public float height = 2;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;
    public GameObject window;

    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = 20;
        }

        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            controller.height = 1.0f;
            speed = 5;
        }

        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            controller.height = 2.0f;
            speed = 10;
        }

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "window")
            {
            Destroy(window);
        }
    }
}
