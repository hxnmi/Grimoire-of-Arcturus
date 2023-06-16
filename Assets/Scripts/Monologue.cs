using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Monologue : MonoBehaviour
{
    int rand;
    [SerializeField] TMP_Text[] text;

    public void MonologueRandom(string mono)
    {

        rand = Random.Range(0, text.Length);
        Debug.Log(rand);
        text[rand].gameObject.SetActive(true);
        text[rand].text = mono;
        StartCoroutine(DisableText(text[rand].gameObject));
    }

    IEnumerator DisableText(GameObject text)
    {
        yield return new WaitForSeconds(3f);
        text.SetActive(false);
    }
}
