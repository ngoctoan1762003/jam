using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thons : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().KnockBack(new Vector2(250, 500));
            collision.gameObject.GetComponent<Player>().TakeDamage(20);
        }
    }
}
