using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public string playerTag = "Player";
    private Transform target;
    public float moveSpeed = .5f;
    public int maxHealth = 2;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
        FindPlayer();
    }

    void Update()
    {
        if (target != null)
        {
            MoveTowardsTarget();
        }
        else
        {
            FindPlayer();
        }
    }

    void MoveTowardsTarget()
    {
        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            Vector3 newPosition = transform.position + (direction * moveSpeed * Time.deltaTime);
            transform.position = newPosition;

            float distanceToPlayer = Vector3.Distance(transform.position, target.position);

            float destroyThreshold = 0.5f;

            if (distanceToPlayer < destroyThreshold)
            {
                Die();
            }
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void FindPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag(playerTag);
        
        if(player != null)
        {
            target = player.transform;
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
