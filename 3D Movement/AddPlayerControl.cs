using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddPlayerControl : MonoBehaviour
{
    Rigidbody _rigidbody;
    [SerializeField] // La hace visible y editable desde Unity.
    Vector3 v3Force;
    [SerializeField]
    KeyCode keyPositive; // Definimos 2 botones.
    [SerializeField]
    KeyCode keyNegative;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>(); 
    }

    // Update is called once per frame
    void FixedUpdate() // Corre 60 veces x segundo, se usa para las fisicas.
    {
        if(Input.GetKey(keyPositive)){
            _rigidbody.velocity += v3Force;
        }
        if(Input.GetKey(keyNegative)){
            _rigidbody.velocity -= v3Force;
        }
    }
}
