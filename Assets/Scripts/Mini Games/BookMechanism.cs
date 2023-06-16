using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class BookMechanism : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] GameObject minigame;
    [SerializeField] GameObject[] theOn;
    [SerializeField] GameObject[] theOff;
    [SerializeField] GameObject panelMiniGame;
    public Image[] m_Image;
    public Sprite[] m_SpriteArray;
    public float m_Speed = .02f;
    private int m_IndexSprite;
    Coroutine m_CorotineAnim;
    GameObject player;
    GameObject[] gos;
    GameObject spawner;
    bool[] isActive;
    bool[] isComplete = new bool[3];
    bool[] completed;
    public bool obeliskOn;
    bool notif = true;

    void Start()
    {
        completed = new bool[3] { true, true, true };
        player = GameObject.FindGameObjectWithTag("Player");
        gos = GameObject.FindGameObjectsWithTag("Enemy");
        spawner = GameObject.FindGameObjectWithTag("EnemySpawner");
    }
    private void Update()
    {
        isActive = minigame.GetComponent<BookSwitch>().IsActive;
        for (int i = 0; i < isActive.Length; i++)
        {
            if (isActive[i] == true)
            {
                theOff[i].SetActive(false);
                theOn[i].SetActive(true);
                isComplete[i] = true;
                StartCoroutine(Func_PlayAnimUI());
            }
            else
            {
                StopCoroutine(Func_PlayAnimUI());
                theOff[i].SetActive(true);
                theOn[i].SetActive(false);
                isComplete[i] = false;
            }
        }

        if (isComplete.SequenceEqual(completed))
        {
            var text = panel.transform.GetChild(4).gameObject;
            if (!text.activeSelf && notif == true)
                text.SetActive(true);
            else
            {
                StartCoroutine(GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayManager>().CompletedNotif());
                notif = false;
            }
            Invoke("CompleteObelisk", 1.5f);
            obeliskOn = true;
        }
    }

    IEnumerator Func_PlayAnimUI()
    {
        yield return new WaitForSeconds(m_Speed);

        if (m_IndexSprite >= m_SpriteArray.Length)
        {
            m_IndexSprite = 0;
        }

        for (int i = 0; i < isActive.Length; i++)
            m_Image[i].sprite = m_SpriteArray[m_IndexSprite];

        m_IndexSprite += 1;
    }

    void CompleteObelisk()
    {
        notif = true;
        var material = player.GetComponent<PlayerInteraction>().Objectinteractable
            .GetComponent<MeshRenderer>()
            .materials[0];
        material.EnableKeyword("_EMISSION");
        material.globalIlluminationFlags = MaterialGlobalIlluminationFlags.None;
        CloseObelisk();
    }

    public void CloseObelisk()
    {
        foreach (GameObject go in gos)
        {
            go.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;
            go.GetComponent<EnemyController>().enabled = true;
        }
        GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayManager>().CompanionOn();
        spawner.GetComponent<SpawnEnemy>().enabled = true;
        player.GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<BookSwitch>().RandomSwitch();
        panelMiniGame.SetActive(false);
    }
}
