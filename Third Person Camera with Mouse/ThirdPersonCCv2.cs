using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// using https://www.youtube.com/watch?v=4HpC--2iowE
public class ThirdPersonCCv2 : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public float speed = 6;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    /// Para la gravity 
    public float gravity = -9.81f;
    Vector3 velocity;
    // Para el ground check
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask; // Layer del piso
    [SerializeField]
    bool isGrounded;
    // Para saltar
    float jumpHeight = 3f;
    // Para correr
    public float normalSpeed = 6f;
    public float runningSpeed = 12f;


    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        // Creates a invisible sphere that if it collides with anything on groundmask its true.

        if(isGrounded && velocity.y < 0)
        {
            // Reseteamos la velocidad.
            velocity.y = -2f;
        }

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        // Para correr
        if (Input.GetKey("left shift") && isGrounded)
        {
            speed = runningSpeed;
        } else
        {
            speed = normalSpeed;
        }
        // Seguimos con el codigo para moverse

        if(direction.magnitude >=  0.1f)
        {
            // Find out how much we have to rotate our character in the Y axis so it looks where he is moving.
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg /* Agregamos esto para q se mueva en la direccion de la camara */ + cam.eulerAngles.y;
            // Aplicamos smoothing asi no snapea brusco.
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
        // Check para saltar
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }
        // Aplicamos Gravedad
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
