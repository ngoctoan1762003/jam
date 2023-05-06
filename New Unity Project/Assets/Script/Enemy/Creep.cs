using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Creep : MonoBehaviour, IDamagable
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
    public HealthBarBehaviour healthBarBehaviour;

    //AI
    public float distance, attackDistance, reverseScale, initialScale, dashSpeed;
    public bool isDashing, isFaceRight, isTouching;
    public Vector2 leftOffset, rightOffset, bottomOffset;
    public LayerMask PlayerMask;

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
                anim.Play("creep_attack");
                attackReady = false;
                isFaceRight = (Player.transform.position.x > transform.position.x) ? true : false;
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

        //Dash
        if (isDashing == true)
        {
            Dash();


            if (isFaceRight == true)
            {
                Collider2D hitPlayer = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset + bottomOffset, attackRadius, PlayerMask);
                Debug.Log("is Facing right");
                if(hitPlayer != null)
                {
                    Debug.Log("Hit");
                    hitPlayer.GetComponent<Player>().TakeDamage(damage);
                    //hitPlayer.GetComponent<Player>().KnockBack(new Vector2(400, 200));
                    isDashing = false;
                }
            }
            else
            {
                Collider2D hitPlayer = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset + bottomOffset, attackRadius, PlayerMask);

                if(hitPlayer != null)
                {
                    Debug.Log("Hit");

                    hitPlayer.GetComponent<Player>().TakeDamage(damage);
                    //hitPlayer.GetComponent<Player>().KnockBack(new Vector2(400, 200));
                    isDashing = false;
                }
            }
        }
        else
        {
            r2D.velocity = new Vector2(r2D.velocity.x * 0.98f, r2D.velocity.y);
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

    public void ToggleDash()
    {
        isDashing = !isDashing;
    }

    void Dash()
    {
        r2D.velocity = new Vector2(0, 0);
        if (isFaceRight == true) r2D.velocity = Vector2.right * dashSpeed;
        if (isFaceRight == false) r2D.velocity = Vector2.right * -dashSpeed;
    }

    void Dead()
    {
        Debug.Log(name + "Dead");
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            isTouching = true;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            isTouching = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            isTouching = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset + bottomOffset, attackRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset + bottomOffset, attackRadius);
        //Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset * 4, radius);
    }
}
