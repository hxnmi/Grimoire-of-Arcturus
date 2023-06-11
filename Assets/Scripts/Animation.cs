using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : MonoBehaviour
{
    [SerializeField] GameObject averyFront;
    [SerializeField] GameObject averyRight;
    [SerializeField] GameObject averyLeft;
    [SerializeField] GameObject averyBack;
    [SerializeField] GameObject grimoire;

    GameObject player;

    GameObject weapon;

    Animator anim;
    string currentState;
    bool walk;

    [System.Obsolete]
    private void Start()
    {
        player = GameObject.Find("Player");
        weapon = player.transform.FindChild("WeaponObj").gameObject;
    }
    // void Update()
    // {
    //     if (weapon.transform.childCount > 0)
    //         return;
    //     else
    //     {
    //         anim = grimoire.GetComponent<Animator>();
    //         ChangeAnimationState("Grimoire_Back");
    //     }
    // }

    public void Animate()
    {
        PlayerMovement playerMovement = GetComponent<PlayerMovement>();
        Rigidbody rb = playerMovement.Rb;
        float horizontalInput = playerMovement.HorizontalInput;
        float verticalInput = playerMovement.VerticalInput;

        //Animation Player
        if (rb.velocity.magnitude > 1f)
        {
            if (verticalInput < 0)
            {
                if (weapon.transform.childCount > 0)
                {
                    anim = weapon.GetComponentInChildren<Animator>();
                    ChangeAnimationState("Grimoire_Front");
                    weapon.transform.localPosition = Vector3.zero;
                }
                averyFront.SetActive(true);
                averyRight.SetActive(false);
                averyBack.SetActive(false);
                averyLeft.SetActive(false);
                anim = averyFront.GetComponent<Animator>();
                ChangeAnimationState("WalkFront_player");
            }
            else if (verticalInput > 0)
            {
                if (weapon.transform.childCount > 0)
                {
                    anim = weapon.GetComponentInChildren<Animator>();
                    ChangeAnimationState("Grimoire_Back");
                    weapon.transform.localPosition = new Vector3(-2f, 0, 0);
                }
                averyBack.SetActive(true);
                averyFront.SetActive(false);
                averyRight.SetActive(false);
                averyLeft.SetActive(false);
                anim = averyBack.GetComponent<Animator>();
                ChangeAnimationState("WalkBack_player");
            }
            else if (horizontalInput > 0)
            {
                if (weapon.transform.childCount > 0)
                {
                    anim = weapon.GetComponentInChildren<Animator>();
                    ChangeAnimationState("Grimoire_Side");
                    weapon.transform.localPosition = Vector3.zero;
                }
                averyRight.SetActive(true);
                averyBack.SetActive(false);
                averyFront.SetActive(false);
                averyLeft.SetActive(false);
                anim = averyRight.GetComponent<Animator>();
                ChangeAnimationState("WalkRight_player");
            }
            else if (horizontalInput < 0)
            {
                if (weapon.transform.childCount > 0)
                {
                    anim = weapon.GetComponentInChildren<Animator>();
                    ChangeAnimationState("Grimoire_Side");
                    weapon.transform.localPosition = new Vector3(-2f, 0, 0);
                }
                averyLeft.SetActive(true);
                averyRight.SetActive(false);
                averyFront.SetActive(false);
                averyBack.SetActive(false);
                anim = averyLeft.GetComponent<Animator>();
                ChangeAnimationState("WalkLeft_player");
            }
        }
        else
        {
            if (anim == averyFront.GetComponent<Animator>())
                ChangeAnimationState("IdleFront_player");
            else if (anim == averyBack.GetComponent<Animator>())
                ChangeAnimationState("IdleBack_player");
            else if (anim == averyRight.GetComponent<Animator>())
                ChangeAnimationState("IdleRight_player");
            else if (anim == averyLeft.GetComponent<Animator>())
                ChangeAnimationState("IdleLeft_player");
        }
    }
    void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;

        anim.Play(newState);

        currentState = newState;
    }
}
