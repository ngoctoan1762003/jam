using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class EnemyBullet : MonoBehaviour, IDamagable
{
    public float damage, speed;
    public Player player;
    public Vector2 direction;
    public Rigidbody2D r2D;
    public float lifeSpan;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        r2D = gameObject.GetComponent<Rigidbody2D>();
        direction = player.transform.position - transform.position;
        direction.Normalize();
    }

    // Update is called once per frame
    void Update()
    {

        lifeSpan -= Time.deltaTime;
        if (lifeSpan <= 0)
        {
            Destroy(gameObject);
        }

        //chase
        Vector2 direction = (Vector2)player.transform.position - (Vector2)transform.position;
        direction.Normalize();
        r2D.velocity = direction * (speed * 0.1f);

    }

    public void TakeDamage(float damage)
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.CompareTag("Player"))
        {
            player.TakeDamage(damage);

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
