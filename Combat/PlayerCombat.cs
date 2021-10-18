using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator m_animator;
    public Transform attackPoint;
    public float attkRange = 0.5f;
    public int AttkDamage = 40;

    public float attckRate = 1.5f; // Times per sec.
    float nextAttckTime = 0f;
    
    // Asignamos enemigos a una layer fija.
    public LayerMask enemyLayers;

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= nextAttckTime) // Esto es la logica para limitar los attaques.
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Attack();
                nextAttckTime = Time.time + 1f / attckRate;
            }
        }
        
    }
    void Attack()
    {
        // Play attk anim.
        m_animator.SetTrigger("Attack");
        // Detect Enemies in range of the attk.
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attkRange, enemyLayers);
        // Applie dmg to the enemies.
        foreach(Collider2D enemy in hitEnemies)
        {
            //Debug.Log("We hit" + enemy.name);
            enemy.GetComponent<Enemy>().TakeDamage(AttkDamage);
        }
    }
    void OnDrawGizmosSelected() // Esto lo usamos para ver el radio del hit.
    {
        if(attackPoint == null){
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attkRange);
    }
}
