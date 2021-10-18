using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolScript : MonoBehaviour
{
    // Creamos una referencia al objeto que vamos a hacer un pool de.
    [SerializeField] private GameObject prefabObject;
    // Creamos un numero para saber que tan grande sera la pool.
    [SerializeField] private int poolDepth;
    [SerializeField] private bool canGrow = true;

    // Creamos una lista, donde guardaremos los objetos de la pool.
    private readonly List<GameObject> pool = new List<GameObject>();

    private void Awake()
    { // Rellenamos la pool en Awake para que todos los objetos que se usen , esten listos antes de que los necesitemos.
        for( int i = 0; i < poolDepth; i++)
        {
            GameObject pooledObject = Instantiate(prefabObject); // Iniciamos el objeto
            pooledObject.SetActive(false); // Lo desactivamos
            pool.Add(pooledObject); // Agregamos el objeto creado a la Lista que creamos antes.
        }
    }

    public GameObject GetAvailableObject() // Este va a ser el metodo de obtener los objetos de nuestra pool.
    {
        for( int i = 0; i< pool.Count; i++)
        {
            if (pool[i].activeInHierarchy == false)
            {
                return pool[i]; // Devuelve el primer objeto inactivo que encuentra en la pool.
            }
        }
        // Esto esta para que si la cantidad de objetos que tenemos en nuestra pool no nos alcanza, se creen nuevos en ese momento. 
        // NO ES RECOMENDABLE DEJARLO PRENDIDO, usarlo para medir cuantos necesitamos y luego apagarlo.
        if (canGrow)
        {
            GameObject pooledObject = Instantiate(prefabObject); 
            pooledObject.SetActive(false); 
            pool.Add(pooledObject);

            return pooledObject;
        }
        else
            return null;
    }
}
