using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISpriteAnimation : MonoBehaviour
{
    [SerializeField] GameObject theBook;
    [SerializeField] GameObject theOn;
    [SerializeField] GameObject theUp;
    public Image m_Image;
    public Sprite[] m_SpriteArray;
    public float m_Speed = .02f;
    private int m_IndexSprite;
    Coroutine m_CorotineAnim;

    public void Update()
    {
        // if (theBook.isActive == true)
        // {
        //     theUp.SetActive(false);
        //     theOn.SetActive(true);
        //     StartCoroutine(Func_PlayAnimUI());
        // }
        // else
        // {
        //     StopCoroutine(Func_PlayAnimUI());
        //     theUp.SetActive(true);
        //     theOn.SetActive(false);
        // }
    }

    IEnumerator Func_PlayAnimUI()
    {
        yield return new WaitForSeconds(m_Speed);

        if (m_IndexSprite >= m_SpriteArray.Length)
        {
            m_IndexSprite = 0;
        }

        m_Image.sprite = m_SpriteArray[m_IndexSprite];

        m_IndexSprite += 1;

        // if (theBook.isActive == false)
        //     m_CorotineAnim = StartCoroutine(Func_PlayAnimUI());
    }
}
