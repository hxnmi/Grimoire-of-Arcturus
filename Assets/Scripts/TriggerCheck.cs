using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCheck : MonoBehaviour
{
    Renderer rend;

    void Start() 
    {
        rend = GetComponent<Renderer>();
    }

    private void OnTriggerStay(Collider other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            rend.enabled = true;
        }
    }

    private void OnTriggerExit() 
    {
        rend.enabled = false;
    }
}
