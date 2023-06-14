using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISpriteAnimation : MonoBehaviour
{
    [SerializeField] GameObject minigame;
    [SerializeField] GameObject[] theOn;
    [SerializeField] GameObject[] theUp;
    public Image[] m_Image;
    public Sprite[] m_SpriteArray;
    public float m_Speed = .02f;
    private int m_IndexSprite;
    Coroutine m_CorotineAnim;
    bool[] isActive;

    private void Update()
    {
        isActive = minigame.GetComponent<BookSwitch>().IsActive;
        for (int i = 0; i < isActive.Length; i++)
            if (isActive[i] == true)
            {
                theUp[i].SetActive(false);
                theOn[i].SetActive(true);
                StartCoroutine(Func_PlayAnimUI());
            }
            else
            {
                StopCoroutine(Func_PlayAnimUI());
                theUp[i].SetActive(true);
                theOn[i].SetActive(false);
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

        // if (theBook.isActive == false)
        //     m_CorotineAnim = StartCoroutine(Func_PlayAnimUI());
    }
}
