using System.Collections;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float speed = 3; 

    private Transform myTransform;
    private float maxVerticalPosition;
    private Vector2 endPosition;

    private string[] makeMeHeavy = new string[750000];
    
  
    private void OnEnable()
        // COMO ES PARTE DE UN POOL, NO PODEMOS USAR START, ya que se correria el codigo 1 sola vez cuando creamos el objeto.
        // Por eso cambiamos Start por OnEnable , para que se corra cada vez que se ejecuta SetActive(true).
    {
        myTransform = transform;

        maxVerticalPosition = Camera.main.ViewportToWorldPoint(new Vector2(Random.value, 1)).y;
        endPosition = new Vector2(myTransform.position.x, maxVerticalPosition + 1);

        StartCoroutine(Move(speed));
    }

    private void Update()
    {
        if (myTransform.position.y > maxVerticalPosition)
        {
            gameObject.SetActive(false); // COMO ES PARTE DE UN POOL , SOLO LO DESACTIVO.
        }
    }
 
    IEnumerator Move(float duration)
    {
        float elapsedTime = 0;
        Vector2 startingPos = myTransform.position;

        while (elapsedTime < duration)
        {
            myTransform.position = Vector3.Lerp(startingPos, endPosition, (elapsedTime / duration));
            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }
    }     
}
