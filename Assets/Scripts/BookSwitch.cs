using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookSwitch : MonoBehaviour
{
    [SerializeField] GameObject[] theBook;
    public void SwitchOn()
    {
        for (int i = 0; i < theBook.Length; i++)
        {
            // if (theBook[i].transform.GetChild(1).gameObject.activeSelf)
            //     theBook[i].isActive = true;
        }
    }

    public void SwitchOff()
    {
        for (int i = 0; i < theBook.Length; i++)
        {
            // if (theBook[i].transform.GetChild(0).gameObject.activeSelf)
            //     theBook[i].isActive = false;
        }
    }
}
