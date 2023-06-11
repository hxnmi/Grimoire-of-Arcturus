using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grimoire : Interactable
{
    [SerializeField] GameObject weapon;

    void MoveGrimoire()
    {
        this.gameObject.transform.SetParent(weapon.transform);
        this.gameObject.transform.localPosition = new Vector3(1f, 1f, 0);
        this.GetComponent<CapsuleCollider>().enabled = false;
        Destroy(gameObject.transform.GetChild(0).gameObject);
        weapon.GetComponentInParent<PlayerCombat>().enabled = true;
    }

    public override string GetDescription()
    {
        return "Press [F] to pick up Grimoire .";
    }

    public override void Interact()
    {
        MoveGrimoire();
    }
}
