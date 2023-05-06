using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindBulletCast : MonoBehaviour
{
    public GameObject bullet;
    public float bulletSpeed;
    public bool isFaceRight;

    public void setDir(bool isFaceRight)
    {
        this.isFaceRight = isFaceRight;
    }

    public void Shoot()
    {
        if (isFaceRight)
        {
            GameObject WindBullet;
            WindBullet = Instantiate(bullet, transform.position, Quaternion.identity) as GameObject;
            WindBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletSpeed, 0);
        }
        else
        {
            GameObject WindBullet;
            WindBullet = Instantiate(bullet, transform.position, Quaternion.identity) as GameObject;
            WindBullet.transform.localScale = new Vector3(WindBullet.transform.localScale.x * -1, WindBullet.transform.localScale.y, WindBullet.transform.localScale.z);
            WindBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(-bulletSpeed, 0);
        }
    }

    public void End()
    {
        Destroy(gameObject);
    }
}
