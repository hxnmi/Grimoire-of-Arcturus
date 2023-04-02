using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
     
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    void Attack()
    {
        //animasi

        //var bullet = Instantiate(bulletPrefabs, BulletSpawnPoint.position, BulletSpawnPoint.rotation);
        //bullet.GetComponent<Rigidbody>().velocity = BulletSpawnPoint.forward * bulletSpeed;
    }
}
