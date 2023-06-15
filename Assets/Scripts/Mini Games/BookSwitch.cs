using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BookSwitch : MonoBehaviour
{
    [SerializeField] GameObject[] theBook;
    [SerializeField] GameObject[] Front;
    [SerializeField] GameObject[] Back;
    [SerializeField] Sprite[] spriteBook;

    bool[] isActive = new bool[3]; public bool[] IsActive { get => isActive; }

    private void Start()
    {
        RandomSwitch();
        Switching();
    }

    public void RandomSwitch()
    {
        for (int i = 0; i < Front.Length; i++)
        {
            int rand = Random.Range(0, 2);
            bool b = System.Convert.ToBoolean(rand);
            Front[i].SetActive(b);
            if (Front[i].activeSelf)
            {
                Back[i].SetActive(false);
            }
            else
            {
                Back[i].SetActive(true);
            }
        }
        for (int i = 0; i < Front.Length; i++)
        {
            int rand = Random.Range(0, 2);
            Front[i].GetComponent<Image>().sprite = spriteBook[rand];
            if (Front[i].GetComponent<Image>().sprite == spriteBook[0])
            {
                Back[i].GetComponent<Image>().sprite = spriteBook[1];
            }
            else
            {
                Back[i].GetComponent<Image>().sprite = spriteBook[0];
            }
        }
    }
    public void Switching()
    {
        for (int i = 0; i < theBook.Length; i++)
        {
            if (theBook[i].transform.GetChild(1).gameObject.activeSelf)
            {
                isActive[i] = true;
            }
            else
            {
                isActive[i] = false;
            }
        }
    }
}
