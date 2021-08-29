using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Animator anim;
    public Transform cam;
    public CharacterController controller;

    public float playerSpeed;
    public float walkingSpeed = 1.5f;
    public float sprintingSpeed = 4.5f;

    public float turnSmoothTime = 0.1f;
    public float acceleration = 2.5f;
    float turnSmoothVelocity;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {        
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
        bool runPressed = Input.GetKey("left shift");

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            if (!runPressed)
            {
                if (playerSpeed < walkingSpeed)
                {
                    playerSpeed += acceleration * Time.deltaTime;
                    //Mathf.Lerp()
                    
                }
                else
                {
                    playerSpeed -= acceleration * Time.deltaTime;
                }
            }
            else
            {
                if (playerSpeed < sprintingSpeed)
                {
                    playerSpeed += acceleration * Time.deltaTime;
                }
            }
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDirection * playerSpeed * Time.deltaTime);
        }
        else
        {
            if (playerSpeed > 0)
            {
                playerSpeed -= 3 * acceleration * Time.deltaTime;
            }
        }

        anim.SetFloat("Speed", playerSpeed);
    }
   
}
