using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public enum Axel
{
    Front,
    Rear
}
[Serializable]
public struct Wheel // Es como una clase con propiedades
{
    public GameObject model;
    public WheelCollider collider;
    public Axel axel; // Para saber si es delantero o trasero.
}

public class carController : MonoBehaviour
{
    [SerializeField] float maxAcceleration = 20f;
    [SerializeField] float turnSensivility = 1f;
    [SerializeField] float maxSteerAngle = 45f;
    [SerializeField] List<Wheel> wheels;
    [SerializeField] Vector3 _centerOfMass;
    float brakes = 0;
    [SerializeField] float brakePower = 500;

    private float inputX, inputY;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = _centerOfMass;
    }

    void Update()
    {
        brakes = 0;
        AnimateWheels();
        GetInputs();
        Brake();
        //Debug.Log(brakes);
    }

    void FixedUpdate()
    {
        Move();
        Turn();
    }

    void Move()
    {
        foreach(var wheel in wheels)
        {
            wheel.collider.motorTorque = inputY * maxAcceleration * 500 *Time.deltaTime;
        }
    }

    void Turn()
    {
        foreach( var wheel in wheels)
        {
            if(wheel.axel == Axel.Front)
            {
                var _steerAngle = inputX * turnSensivility * maxSteerAngle;
                wheel.collider.steerAngle = _steerAngle;
            }
        }
    }

    void Brake()
    {
        if(Input.GetKey(KeyCode.Space) == true)
        {
            brakes = brakePower;
            //Debug.Log("Braking");
        }
        foreach (var wheel in wheels)
            {
                //if(wheel.axel == Axel.Rear)
                //{
                    wheel.collider.brakeTorque = brakes;                    
                //}
            }
    }

    void GetInputs()
    {
        inputX = Input.GetAxis("Horizontal");
        inputY = Input.GetAxis("Vertical");
    }

    void AnimateWheels()
    {
        foreach (var wheel in wheels)
        {
            Quaternion _rotation;
            Vector3 _position;
            wheel.collider.GetWorldPose(out _position, out _rotation);
            wheel.model.transform.position = _position;
            wheel.model.transform.rotation = _rotation;
        }
    }
}
