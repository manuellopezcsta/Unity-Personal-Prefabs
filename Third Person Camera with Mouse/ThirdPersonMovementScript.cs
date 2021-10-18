using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// using https://www.youtube.com/watch?v=4HpC--2iowE
public class ThirdPersonMovementScript : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public float speed = 6;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;


    void Update()
    {

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

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
    }
}
