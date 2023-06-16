using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class CheckTriggerMonologue : MonoBehaviour
{
    [TextArea(3, 10)]
    public string mono;
    bool disableText = false;
    [SerializeField] AudioClip monologue;
    private void OnTriggerEnter(Collider other)
    {
        {
            if (other.gameObject.CompareTag("Player") && !disableText)
            {
                SoundManager.Instance.PlayMonologue(monologue);
                transform.parent.gameObject.GetComponent<Monologue>().MonologueRandom(mono);
                disableText = true;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && disableText)
        {
            disableText = false;
        }
    }
}