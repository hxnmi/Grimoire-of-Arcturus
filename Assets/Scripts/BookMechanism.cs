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
	[SerializeField] GameObject[] theUp;
	public Image[] m_Image;
	public Sprite[] m_SpriteArray;
	public float m_Speed = .02f;
	private int m_IndexSprite;
	Coroutine m_CorotineAnim;
	bool[] isActive;
	bool[] isComplete = new bool[3];
	bool[] completed;

	void Start()
	{
		completed = new bool[3] { true, true, true };
	}
	private void Update()
	{
		isActive = minigame.GetComponent<BookSwitch>().IsActive;
		for (int i = 0; i < isActive.Length; i++)
		{
			if (isActive[i] == true)
			{
				theUp[i].SetActive(false);
				theOn[i].SetActive(true);
				isComplete[i] = true;
				StartCoroutine(Func_PlayAnimUI());
			}
			else
			{
				StopCoroutine(Func_PlayAnimUI());
				theUp[i].SetActive(true);
				theOn[i].SetActive(false);
				isComplete[i] = false;
			}
		}

		if (isComplete.SequenceEqual(completed))
		{
			panel.SetActive(false);
			isComplete = new bool[3] { false, false, false };
			for (int i = 0; i < isActive.Length; i++)
			{
				StopCoroutine(Func_PlayAnimUI());
				theUp[i].SetActive(true);
				theOn[i].SetActive(false);
			}


			GameObject[] gos = GameObject.FindGameObjectsWithTag("Enemy");
			foreach (GameObject go in gos)
			{
				go.GetComponent<NavMeshAgent>().enabled = true;
				go.GetComponent<EnemyController>().enabled = true;
			}
			GameObject spawner = GameObject.FindGameObjectWithTag("Enemy");
			spawner.SetActive(true);
			GameObject player = GameObject.FindGameObjectWithTag("Player");
			player.GetComponent<PlayerInteraction>().Objectinteractable
					  .GetComponent<MeshRenderer>()
					  .materials[0]
					  .EnableKeyword("_EMISSION");
			player.GetComponent<Rigidbody>().isKinematic = false;
			GameObject companion = GameObject.FindGameObjectWithTag("Companion");
			companion.GetComponent<NavMeshAgent>().enabled = true;
			companion.GetComponent<CompanionController>().enabled = true;
		}
		else
			return;
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
}
