using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    public Transform objectToFollow;
    [SerializeField] Vector3 offset;
    [SerializeField] float followSpeed = 10;
    [SerializeField] float lookSpeed = 10;
    public void LookAtTarget()
    {
        Vector3 _lookDirection = objectToFollow.position - transform.position;
        Quaternion _rotation = Quaternion.LookRotation(_lookDirection, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, _rotation, lookSpeed * Time.deltaTime);
    }

    public void MoveToTarget()
    {
        Vector3 _targetPos = objectToFollow.position + 
                             objectToFollow.forward * offset.z +
                             objectToFollow.right * offset.x +
                             objectToFollow.up * offset.y;
        transform.position = Vector3.Lerp(transform.position, _targetPos, followSpeed * Time.deltaTime);
    }

    void FixedUpdate()
    {
        LookAtTarget();
        MoveToTarget();
    }
}
