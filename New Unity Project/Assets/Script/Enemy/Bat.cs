using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour, IDamagable
{
    public GameObject Player;
    public float distance, attackDistance;
    public Rigidbody2D r2D;
    public float speed;

    public GameObject Bullet;
    public float attackDuration, attackCurrentTime;
    public bool attackReady = true;

    public float maxHealth = 100;
    public float currentHealth;

    public HealthBarBehaviour healthBarBehaviour;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        r2D = gameObject.GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        healthBarBehaviour.setHealth(currentHealth, maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        //--ChasePlayer--------------------
        if (Vector2.Distance((Vector2)transform.position, (Vector2)Player.transform.position) <= distance &&
            Vector2.Distance((Vector2)transform.position, (Vector2)Player.transform.position) >= attackDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, speed * Time.deltaTime);
        }

        if (Vector2.Distance((Vector2)transform.position, (Vector2)Player.transform.position) < attackDistance)
        {
            if (attackReady == true)
            {
                GameObject bulletClone = Instantiate(Bullet, transform.position, Quaternion.identity) as GameObject;
            }
            attackReady  = false;
        }

        if (attackReady == false)
        {
            if (attackCurrentTime <= 0 && attackReady == false)
            {
                attackReady = true;
                attackCurrentTime = attackDuration;
            }
            attackCurrentTime -= Time.deltaTime;
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthBarBehaviour.setHealth(currentHealth, maxHealth);
        if (currentHealth <= 0)
        {
            Dead();
        }
    }

    void Dead()
    {
        Debug.Log(name + "Dead");
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position, attackDistance);
        Gizmos.DrawWireSphere((Vector2)transform.position, distance);

        //Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset * 4, radius);
    }
}

