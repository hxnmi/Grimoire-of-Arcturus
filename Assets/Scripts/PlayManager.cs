using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManager : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] GameObject Gate;
    [SerializeField] GameObject spawner;
    [SerializeField] Material skyboxMat;
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

    public void Restoration()
    {
        GetComponent<Animation>().Companion.transform.GetChild(1).gameObject.SetActive(false);
        GetComponent<Animation>().Player.transform.GetChild(2).gameObject.SetActive(true);
        RenderSettings.skybox = skyboxMat;
        spawner.SetActive(false);
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject go in gos)
        {
            go.GetComponent<Enemy>().Die();
        }
    }

    IEnumerator comGetTag(GameObject companion)
    {
        yield return new WaitForSeconds(2f);
        companion.transform.GetChild(0).tag = "Interactable";
    }

    public IEnumerator CompletedNotif()
    {
        //need sound
        yield return new WaitForSeconds(1f);
        panel.transform.GetChild(4).gameObject.SetActive(false);
    }
}
