using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckAttackRange : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.CompareTag("Player"))
        {
            gameObject.transform.parent.GetComponent<IDamagable>().InRange();
            Debug.Log("in range");
        }
    }

    private void OnTriggerExit2D(Collider2D collider2D)
    {
        if (collider2D.CompareTag("Player"))
        {
            gameObject.transform.parent.GetComponent<IDamagable>().OutRange();
            Debug.Log("out range");
        }
    }
}
