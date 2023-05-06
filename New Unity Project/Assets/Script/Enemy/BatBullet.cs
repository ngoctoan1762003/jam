using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class BatBullet : MonoBehaviour, IDamagable
{
    Rigidbody2D rb;
    GameObject player;
    public float speed;
    public float lifeSpan;
    public float damage;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        Vector2 moveDir = (player.transform.position - rb.transform.position).normalized * speed;
        rb.velocity = moveDir;
        Destroy(gameObject, lifeSpan);
    }

    public void TakeDamage(float damage)
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.CompareTag("Player"))
        {
            player.GetComponent<Player>().TakeDamage(damage);

            if (player.transform.position.x > transform.position.x)
            {
                player.GetComponent<Player>().DamagedKnockBackForce(new Vector2(150, 100));
            }
            else
            {
                player.GetComponent<Player>().DamagedKnockBackForce(new Vector2(-150, 100));
            }
            Destroy(gameObject);
        }
        if (collider2D.CompareTag("EarthShield"))
        {
            collider2D.gameObject.GetComponent<EarthShield>().TakeDamage(damage);
            Destroy(gameObject);
        }

    }
}
