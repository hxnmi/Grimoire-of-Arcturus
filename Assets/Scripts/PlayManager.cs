using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManager : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] GameObject Gate;
    public List<BookMechanism> obeliskOn;
    void Update()
    {
        foreach (BookMechanism i in obeliskOn)
        {
            if (i.obeliskOn == false)
                return;
            else
            {
                Gate.transform.GetChild(2).GetChild(0).GetComponent<Rigidbody>().isKinematic = false;
                Gate.transform.GetChild(3).GetChild(0).GetComponent<Rigidbody>().isKinematic = false;
                Gate.transform.parent.GetChild(1).GetChild(0).GetComponent<MeshRenderer>().materials[0].EnableKeyword("_EMISSION");
            }
        }
    }

    public void CompanionOn()
    {
        GameObject companion = GetComponent<Animation>().Companion;
        companion.SetActive(true);
        StartCoroutine(comGetTag(companion));
    }

    IEnumerator comGetTag(GameObject companion)
    {
        yield return new WaitForSeconds(2f);
        companion.transform.GetChild(0).tag = "Interactable";
    }
}
