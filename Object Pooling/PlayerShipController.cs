using UnityEngine;

public class PlayerShipController : MonoBehaviour
{
    private ObjectPoolScript objectPool;
    private Transform myTransform;
  
    // Start is called before the first frame update
    void Start()
    {
        myTransform = transform;

        objectPool = GetComponent<ObjectPoolScript>(); // Iniciamos la pool en el objeto que la quiera usar.

        InvokeRepeating("Shoot", .33f, .33f);
    }

    void Shoot()
    {
        GameObject bullet = objectPool.GetAvailableObject(); // Agarramos un objeto de la pool
        bullet.transform.position = myTransform.position; // Cambiamos su posicion a la de la nave
        bullet.SetActive(true); // Lo activamos.
    }
}

// Cuando usas object Pooling.
// No destruir los objetos, solo desactivarlos.
// Cambiar el transform al que necesite.
// Activar el objeto.
// Usar OnEnable en vez de Start para los prefabs, para poder correr ese codigo multiples veces.