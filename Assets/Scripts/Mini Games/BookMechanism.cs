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
            StartCoroutine(CompletedNotif());
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
        player.GetComponent<PlayerInteraction>().Objectinteractable
            .GetComponent<MeshRenderer>()
            .materials[0]
            .EnableKeyword("_EMISSION");
        CloseObelisk();
    }

    IEnumerator panelClosedDelay()
    {
        yield return new WaitForSeconds(1f);

        yield return new WaitForSeconds(0.5f);

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

    IEnumerator CompletedNotif()
    {
        //need sound
        yield return new WaitForSeconds(0.5f);
        panel.transform.GetChild(4).gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        panel.transform.GetChild(4).gameObject.SetActive(false);
    }
}
