using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Collections.AllocatorManager;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Player_Health : MonoBehaviour
{
    public int maxHealth = 500;
    public int currentHealth;
    public Animator animator;
    Rigidbody2D rig;
    public CapsuleCollider2D col;

    public Player_HealthBar player_HealthBar;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        rig = GetComponent<Rigidbody2D>();

        player_HealthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        player_HealthBar.SetHealth(currentHealth);

        animator.SetTrigger("isHurt");
        

        //Instantiate(blood, transform.position, Quaternion.identity);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {

        //Instantiate(deathEffect, transform.position, Quaternion.identity);
        animator.SetBool("isDead", true);
        GetComponent<Collider2D>().enabled = false; //Disable the collider 2D
        this.enabled = false; //Disable the enemy script
                              //Destroy(gameObject);

        FindObjectOfType<GameSession>().PlayerDeath();
    }

    public void OnTriggerEnter2D(Collider2D player)
    {
        if (player.gameObject.CompareTag("Spike"))
        {
            //animator.SetTrigger("isHurt");
            GetComponent<Player_Health>().TakeDamage(25);
            //gameObject.SetActive(false);
            //Destroy(gameObject);
        }
    }
    }
