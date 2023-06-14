using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCheckTerrain : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<CapsuleCollider>().enabled = false;
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(GameObject.FindGameObjectWithTag("Enemy"));
        }
    }
}
