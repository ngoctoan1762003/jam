using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour, IDamagable
{
    public Rigidbody2D r2D;

    public float maxHealth = 100;
    public float currentHealth;

    public GameObject Player;
    public float speed;

    public LayerMask playerMask;
    public float attackRadius, damage, attackDuration, attackCurrentTime;
    public bool attackReady = true;
    public bool isInRange = false;

    public Animator anim;
    public GameObject bullet;
    public HealthBarBehaviour healthBarBehaviour;

    //AI
    public float distance, attackDistance, reverseScale, initialScale;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        Player = GameObject.FindGameObjectWithTag("Player");
        currentHealth = maxHealth;
        r2D = gameObject.GetComponent<Rigidbody2D>();
        healthBarBehaviour.setHealth(currentHealth, maxHealth);
        initialScale = transform.localScale.x;
        reverseScale = -transform.localScale.x;
    }

    private void Update()
    {
        //attack-----------------------------
        if (isInRange)
        {
            if (Player.transform.position.x > transform.position.x)
            {
                transform.localScale = new Vector3(reverseScale, transform.localScale.y, transform.localScale.z);
            }
            else
            {
                transform.localScale = new Vector3(initialScale, transform.localScale.y, transform.localScale.z);
            }

            if (attackReady == true)
            {
                anim.Play("bow_attack");
                attackReady = false;
            }
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

    public void InRange()
    {
        isInRange = true;
    }

    public void OutRange()
    {
        isInRange = false;
    }

    public void Shoot()
    {
        GameObject bulletClone = Instantiate(bullet, gameObject.transform.position, Quaternion.identity);
    }

    void Dead()
    {
        Debug.Log(name + "Dead");
        Destroy(gameObject);
    }



}
