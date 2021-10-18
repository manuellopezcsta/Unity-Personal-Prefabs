using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int maxHealth = 100;
    int currentHealth;

    private Animator m_animator;
    private BoxCollider2D m_collider;

    // Start is called before the first frame update
    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_collider =  GetComponent<BoxCollider2D>();
        currentHealth = maxHealth;
    }

    // Creamos una funcion publica para llamarla desde el otro script.
    public void TakeDamage (int damage)
    {
        currentHealth -= damage;
        // Play hurt animation
        m_animator.SetTrigger("Hurt");
        
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        //Play de animation.
        m_animator.SetTrigger("Death");
        // Disable the Enemy.
        Debug.Log("Enemy died");
        GetComponent<Rigidbody2D>().simulated = false; 
        m_collider.enabled = false;
    }
}
