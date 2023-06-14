using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookSwitch : MonoBehaviour
{
    [SerializeField] GameObject[] theBook;

    bool[] isActive = new bool[3]; public bool[] IsActive { get => isActive; }

    private void Switching()
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

    // private void SwitchOff()
    // {
    //     for (int i = 0; i < theBook.Length; i++)
    //     {
    //         if (theBook[i].transform.GetChild(0).gameObject.activeSelf)
    //             isActive[i] = false;
    //     }
    // }

    // public void Switching()
    // {
    //     SwitchOn();
    // 	SwitchOff();
    // }
}
